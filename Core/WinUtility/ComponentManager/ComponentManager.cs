using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using BrightIdeasSoftware;
using XCI.Component;
using XCI.Core;
using XCI.Helper;

namespace XCI.WinUtility.ComponentManager
{
    /// <summary>
    /// 类型管理
    /// </summary>
    public static class TypeManager
    {
        private static XCIList<InterfaceEntity> ComponentList = new XCIList<InterfaceEntity>();

        static TypeManager()
        {
            LoadData(InitInterface);
            LoadData(InitClass);
        }

        public static XCIList<InterfaceEntity> Data
        {
            get
            {
                return ComponentList;
            }
        }

        private static Size ImageSize = new Size(64, 64);

        private static Image _defaultImage;

        public static Image DefaultImage
        {
            get
            {
                if (_defaultImage == null)
                {
                    var im = ResourceImageHelper.CreateBitmapFromResources("XCI.XCIComponent.ComponentLogo.png",
                                                                           typeof(IManager).Assembly);
                    if (im != null)
                    {
                        _defaultImage = new Bitmap(im, ImageSize);
                    }
                }
                return _defaultImage;
            }
        }

        private static ImageList _larImageList;

        public static ImageList LarImageList
        {
            get
            {
                if (_larImageList == null)
                {
                    _larImageList = new ImageList();
                    _larImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
                    _larImageList.ImageSize = ImageSize;
                    _larImageList.TransparentColor = System.Drawing.Color.Transparent;
                    foreach (var item in ComponentList)
                    {
                        foreach (var classEntity in item.ClassEntityList)
                        {
                            string key = classEntity.Icon;
                            if (!_larImageList.Images.ContainsKey(key))
                            {
                                if (key.Equals("Default") && TypeManager.DefaultImage != null)
                                {
                                    _larImageList.Images.Add(key, TypeManager.DefaultImage);
                                }
                                else
                                {
                                    var im = ResourceImageHelper.CreateBitmapFromResources(key, classEntity.ClassType.Assembly);
                                    if (im != null)
                                    {
                                        var img = new Bitmap(im, ImageSize);
                                        _larImageList.Images.Add(key, img);
                                    }
                                }
                            }
                        }
                    }
                }
                return _larImageList;
            }
        }

        public static XCIList<string> GetAssemblyFileList()
        {
            XCIList<string> files = new XCIList<string>();
            var list = Directory.GetFiles(Application.StartupPath);
            files.AddRange(list);
            string pluginPath = Application.StartupPath + "\\Plugin";
            FileHelper.CreateDirectory(pluginPath);
            list = Directory.GetFiles(pluginPath);
            files.AddRange(list);
            pluginPath = Application.StartupPath + "\\Component";
            FileHelper.CreateDirectory(pluginPath);
            list = Directory.GetFiles(pluginPath);
            files.AddRange(list);
            return files;
        }

        public static XCIList<Type> GetTypeList(Type baseClassType)
        {
            XCIList<Type> typeList = new XCIList<Type>();
            Action<Assembly> action = p =>
            {
                IList<Type> types = AssemblyHelper.GetTypeByBase(p, baseClassType);
                foreach (Type t in types)
                {
                    typeList.AddOrUpdate(t);
                }
            };
            LoadData(action);
            return typeList;
        }

        private static void LoadData(Action<Assembly> action)
        {
            var files = GetAssemblyFileList();

            foreach (string file in files)
            {
                if (IsPreCompiled(file))
                {
                    string assemblyName = Path.GetFileNameWithoutExtension(file);
                    if (assemblyName != null)
                    {
                        try
                        {
                            Assembly assembly = Assembly.Load(assemblyName);
                            action(assembly);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private static void InitInterface(Assembly assembly)
        {
            Type managerType = typeof(IManager);
            IList<Type> types = AssemblyHelper.GetInterfaceType(assembly, managerType);
            foreach (Type t in types)
            {
                InterfaceEntity entity = BuildInterfaceEntity(t);
                ComponentList.AddOrUpdate(entity);
            }
        }

        private static void InitClass(Assembly assembly)
        {
            foreach (var item in ComponentList)
            {
                var typeList = AssemblyHelper.GetClassType(assembly, item.InterfaceType);
                foreach (var t in typeList)
                {
                    ClassEntity cls = BuildClassEntity(t);
                    item.ClassEntityList.AddOrUpdate(cls);
                }
            }
        }

        public static InterfaceEntity BuildInterfaceEntity(Type t)
        {
            InterfaceEntity entity = new InterfaceEntity();
            entity.ComponentAttribute = AssemblyHelper.GetCustomAttributes<XCIComponentDescriptionAttribute>(t);
            entity.InterfaceType = t;
            entity.Provider = AssemblyHelper.GetTypeFullName(t);
            entity.ClassEntityList = new XCIList<ClassEntity>();
            return entity;
        }

        public static ClassEntity BuildClassEntity(Type t)
        {
            ClassEntity entity = new ClassEntity();
            entity.ComponentAttribute = AssemblyHelper.GetCustomAttributes<XCIComponentAttribute>(t);
            entity.ClassType = t;
            entity.Provider = AssemblyHelper.GetTypeFullName(t);
            return entity;
        }

        public static bool IsPreCompiled(string path)
        {
            string fileExtension = Path.GetExtension(path);
            bool result = (fileExtension.Equals(".dll") || fileExtension.Equals(".exe"))
                          && path.IndexOf("vshost") == -1;
            return result;
        }

    }

    public class InterfaceEntity
    {
        public string Title
        {
            get
            {
                if (ComponentAttribute != null)
                {
                    return ComponentAttribute.Name;
                }
                return string.Empty;
            }
        }

        public string Provider { get; set; }

        public string Group
        {
            get
            {
                if (ComponentAttribute != null)
                {
                    return ComponentAttribute.Group;
                }
                return "默认";
            }
        }

        public Type InterfaceType { get; set; }

        public XCIComponentDescriptionAttribute ComponentAttribute { get; set; }

        public XCIList<ClassEntity> ClassEntityList { get; set; }

        public override bool Equals(object obj)
        {
            return this.Provider.Equals(((InterfaceEntity)obj).Provider);
        }

        public override int GetHashCode()
        {
            return this.Provider.GetHashCode();
        }

    }

    public class ClassEntity
    {
        private string _title;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                if (_title == null && ComponentAttribute != null)
                {
                    _title = ComponentAttribute.Name;
                }
                return _title;
            }
            set { _title = value; }
        }

        public string Icon
        {
            get
            {
                string result = null;
                if (ComponentAttribute != null)
                {
                    result = ComponentAttribute.Logo;
                }
                if (string.IsNullOrEmpty(result))
                {
                    result = "Default";
                }
                return result;
            }
        }

        /// <summary>
        /// 提供程序 格式:{命名空间.类名称,程序集名称}
        /// </summary>
        public string Provider { get; set; }

        public Type ClassType { get; set; }

        public XCIComponentAttribute ComponentAttribute { get; set; }

        public override bool Equals(object obj)
        {
            return this.Provider.Equals(((ClassEntity)obj).Provider);
        }

        public override int GetHashCode()
        {
            return this.Provider.GetHashCode();
        }
    }
}
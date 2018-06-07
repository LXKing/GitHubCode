using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XCI.Component
{
    public partial class frmEncryptTest : Form
    {
        public frmEncryptTest(string configName)
        {
            InitializeComponent();
            this.ConfigName = configName;
            CheckForIllegalCrossThreadCalls = false;
        }

        public string ConfigName { get; set; }

        private ConfigEntity _config;
        public ConfigEntity Config
        {
            get
            {
                if (_config == null)
                {
                    _config = ConfigFactory.Current.GetConfig(EncryptFactory.Factory.InterfaceName, ConfigName);
                }
                return _config;
            }
        }

        private IEncrypt _instance;
        public IEncrypt Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = EncryptFactory.Factory.Get(ConfigName);
                    ConfigFactory.SetProviderProperty(_instance, Config);
                    return _instance;
                }
                return _instance;
            }
        }

        private void frmEncryptTest_Load(object sender, EventArgs e)
        {
            this.txtInternalKey.Text = Instance.InternalKey;
            this.cbIsEncrypt.Checked = Instance.IsEncrypt;
            if (Config.ComponentAttribute != null)
            {
                txtTitle.Text = Config.ComponentAttribute.Name + "  ";
                txtAuthorInfo.Text = Config.ComponentAttribute.Author
                    + "  " + Config.ComponentAttribute.Version
                    + "  " + Config.ComponentAttribute.Contact;
                picLogo.Image = Config.Logo;
            }

            txtTitle.Text += Config.Provider;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string plaintext = txtPlainText.Text.Trim();
            if (plaintext.Length > 0)
            {
                SetProviderProperty();
                try
                {
                    this.txtEncryptText.Text = Instance.Encrypt(plaintext);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string encrypttext = txtEncryptText.Text.Trim();
            if (encrypttext.Length > 0)
            {
                SetProviderProperty();
                try
                {
                    this.txtPlainText.Text = Instance.Decrypt(encrypttext);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void SetProviderProperty()
        {
            Instance.InternalKey = txtInternalKey.Text.Trim();
            Instance.IsEncrypt = cbIsEncrypt.Checked;
            EncryptFactory.Factory.SetPropertyToConfig(ConfigName, Config);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPlainText.Text = string.Empty;
            txtEncryptText.Text = string.Empty;
            txtPlainText.Select();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtEncryptText.Text.Trim().Length > 0)
            {
                Clipboard.SetText(txtEncryptText.Text.Trim());
            }
        }
    }
}

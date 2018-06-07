namespace DevLocalization
{
    using DevExpress.XtraCharts.Localization;
    using System;

    public class ChartLocalizer_zhchs : ChartResLocalizer
    {
        public override string GetLocalizedString(ChartStringId id)
        {
            switch (id)
            {
                case ChartStringId.SeriesPrefix:
                    return "系列";

                case ChartStringId.PalettePrefix:
                    return "调色板";

                case ChartStringId.XYDiagramPanePrefix:
                    return "窗格";

                case ChartStringId.SecondaryAxisXPrefix:
                    return "二级X坐标";

                case ChartStringId.SecondaryAxisYPrefix:
                    return "二级Y坐标";

                case ChartStringId.ConstantLinePrefix:
                    return "常量行";

                case ChartStringId.CustomAxisLabelPrefix:
                    return "标签";

                case ChartStringId.ScaleBreakPrefix:
                    return "刻度中止";

                case ChartStringId.AnnotationPrefix:
                    return "批注";

                case ChartStringId.ImageAnnotationPrefix:
                    return "图像批注";

                case ChartStringId.TextAnnotationPrefix:
                    return "文本批注";

                case ChartStringId.StripPrefix:
                    return "带";

                case ChartStringId.StackedGroupPrefix:
                    return "组";

                case ChartStringId.ArgumentMember:
                    return "参数";

                case ChartStringId.ValueMember:
                    return "值";

                case ChartStringId.WeightMember:
                    return "重量";

                case ChartStringId.LowValueMember:
                    return "低";

                case ChartStringId.HighValueMember:
                    return "高";

                case ChartStringId.OpenValueMember:
                    return "打开";

                case ChartStringId.CloseValueMember:
                    return "关闭";

                case ChartStringId.AutocreatedSeriesName:
                    return "自动创建系列";

                case ChartStringId.DefaultDataFilterName:
                    return "数据过滤器";

                case ChartStringId.DefaultChartTitle:
                    return "图表标题";

                case ChartStringId.DefaultAnnotation:
                    return "批注";

                case ChartStringId.DefaultSeriesPointFilterName:
                    return "系列点过滤器";

                case ChartStringId.MsgEquallySpacedItemsNotUsable:
                    return "在方向属性设置为{0}时，该属性不能使用。";

                case ChartStringId.MsgSeriesViewDoesNotExist:
                    return "{0}系列视图不存在。";

                case ChartStringId.MsgEmptyArrayOfValues:
                    return "此值数组为空。";

                case ChartStringId.MsgItemNotInCollection:
                    return "采集中不包含该指定项目。";

                case ChartStringId.MsgIncorrectIndentFromMarker:
                    return "缩进值应该大于等于0";

                case ChartStringId.MsgIncorrectValue:
                    return "值\"{0}\"不是参数\"{1}\"的正确值。";

                case ChartStringId.MsgIncompatiblePointType:
                    return "\"{0}\"点的类型和{1}标度不兼容。";

                case ChartStringId.MsgIncompatibleArgumentDataMember:
                    return "\"{0}\"参数数据成员的类型和{1}标度不兼容。";

                case ChartStringId.MsgIncompatibleValueDataMember:
                    return "\"{0}\"值数据成员的类型和{1}标度不兼容。";

                case ChartStringId.MsgDesignTimeOnlySetting:
                    return "程序运行时不能设置此项属性。";

                case ChartStringId.MsgInvalidDataSource:
                    return "无效的数据源类型(没有支持的接口可执行)。";

                case ChartStringId.MsgIncorrectDataMember:
                    return "数据源中不包含一个名为\"{0}\"的数据成员。";

                case ChartStringId.MsgInvalidSortingKey:
                    return "不能将排序键的值设置为{0}。";

                case ChartStringId.MsgInvalidFilterCondition:
                    return "{0}情况不能应用\"{1}\"数据。";

                case ChartStringId.MsgIncorrectDataAdapter:
                    return "{0}对象不是一个数据适配器。";

                case ChartStringId.MsgDataSnapshot:
                    return "数据快照已完成。注意，所有序列的数据仍在图表中，图表处于未绑定模式。";

                case ChartStringId.MsgModifyDefaultPaletteError:
                    return "默认使用该调色板，且不能修改该调色板。";

                case ChartStringId.MsgAddExistingPaletteError:
                    return "存储器中已经存在名称为{0}的调色板。";

                case ChartStringId.MsgInternalPropertyChangeError:
                    return "这个属性只能供内部使用。不允许改变它的值。";

                case ChartStringId.MsgPaletteNotFound:
                    return "图表在不包含名称为{0}的调色板。";

                case ChartStringId.MsgLabelSettingRuntimeError:
                    return "在运行时不能设置\"标记\"属性。";

                case ChartStringId.MsgPointOptionsSettingRuntimeError:
                    return "在运行时不能设置\"点选项\"属性。";

                case ChartStringId.MsgLegendPointOptionsSettingRuntimeError:
                    return "\"LegendPointOptions\"属性不能在运行时设置。";

                case ChartStringId.MsgSynchronizePointOptionsSettingRuntimeError:
                    return "\"SynchronizePointOptions\"属性不能在运行时设置。";

                case ChartStringId.MsgIncorrectNumericPrecision:
                    return "精度应该大于等于0。";

                case ChartStringId.MsgIncorrectAxisThickness:
                    return "轴的厚度应该大于0。";

                case ChartStringId.MsgIncorrectBarWidth:
                    return "柱状条的宽度应该大于0。";

                case ChartStringId.MsgIncorrectBarDepth:
                    return "柱状条的深度应该大于0。";

                case ChartStringId.MsgIncorrectLineWidth:
                    return "线宽应该大于0。";

                case ChartStringId.MsgIncorrectAreaWidth:
                    return "面积的宽度应该大于0。";

                case ChartStringId.MsgIncorrectBorderThickness:
                    return "边沿宽度应该大于0。";

                case ChartStringId.MsgIncorrectChartTitleIndent:
                    return "标题缩进应该大于等于0且小于1000。";

                case ChartStringId.MsgIncorrectMaxLineCount:
                    return "最大线数应该大于等于0小于等于20";

                case ChartStringId.MsgIncorrectLegendMarkerSize:
                    return "图例记号的大小应该大于0且小于1000。";

                case ChartStringId.MsgIncorrectLegendHorizontalIndent:
                    return "图例水平缩进应大于或等于0且小于1000。";

                case ChartStringId.MsgIncorrectLegendVerticalIndent:
                    return "图例垂直缩进应大于或等于0且小于1000。";

                case ChartStringId.MsgIncorrectLegendTextOffset:
                    return "图例文本偏移应大于或等于0且小于1000。";

                case ChartStringId.MsgIncorrectMarkerSize:
                    return "标记大小应该大于１。";

                case ChartStringId.MsgIncorrectMarkerStarPointCount:
                    return "星点的数量应该大于3且小于101。";

                case ChartStringId.MsgIncorrectPieSeriesLabelColumnIndent:
                    return "列缩进应该大于等于0。";

                case ChartStringId.MsgIncorrectRangeBarSeriesLabelIndent:
                    return "缩进量应该大于等于0。";

                case ChartStringId.MsgIncorrectPercentPrecision:
                    return "百分数值的精度应该大于0。";

                case ChartStringId.MsgIncorrectPercentageAccuracy:
                    return "百分比的精确度应该大于0";

                case ChartStringId.MsgIncorrectSeriesLabelLineLength:
                    return "线长应该大于等于0且小于1000。";

                case ChartStringId.MsgIncorrectStripConstructorParameters:
                    return "标记线的最大极限和最小极限不能相同。";

                case ChartStringId.MsgIncorrectStripLimitAxisValue:
                    return "StripLimit对象的轴值属性不能设置为空。";

                case ChartStringId.MsgIncorrectStripMinLimit:
                    return "标记线的最小极限必须小于最大极限。";

                case ChartStringId.MsgIncorrectStripMaxLimit:
                    return "标记线的最大极限必须大于最小极限。";

                case ChartStringId.MsgIncorrectLineThickness:
                    return "线厚应该大于0。";

                case ChartStringId.MsgIncorrectShadowSize:
                    return "阴影大小应该大于0。";

                case ChartStringId.MsgIncorrectTickmarkThickness:
                    return "刻度的厚度应该大于0。";

                case ChartStringId.MsgIncorrectTickmarkLength:
                    return "刻度的长度应该大于0。";

                case ChartStringId.MsgIncorrectTickmarkMinorThickness:
                    return "最小刻度的厚度应该大于0。";

                case ChartStringId.MsgIncorrectTickmarkMinorLength:
                    return "最小刻度的长度应该大于0。";

                case ChartStringId.MsgIncorrectMinorCount:
                    return "最小计数的数量应该大于0且小于100。";

                case ChartStringId.MsgIncorrectPercentValue:
                    return "百分数值应该大于等于0且小于等于100。";

                case ChartStringId.MsgIncorrectHeightToWidthRatioValue:
                    return "高宽比属性值不正确，应该在0.1到10";

                case ChartStringId.MsgIncorrectPointDistance:
                    return "点距离值应该大于0";

                case ChartStringId.MsgIncorrectSimpleDiagramDimension:
                    return "简单图表的尺度应该大于0且小于100。";

                case ChartStringId.MsgIncorrectStockLevelLineLengthValue:
                    return "股指线的长度值应该大于等于0。";

                case ChartStringId.MsgIncorrectReductionColorValue:
                    return "减少颜色值不能为空。";

                case ChartStringId.MsgIncorrectLabelAngle:
                    return "标记的角度大小应该大于等于-360度且小于等于360度。";

                case ChartStringId.MsgIncorrectImageBounds:
                    return "不能创建指定大小的图象。";

                case ChartStringId.MsgIncorrectUseImageUrlProperty:
                    return "ImageUrl属性不能用于网络图表控件，请使用图象属性替代。";

                case ChartStringId.MsgIncorrectSeriesDistance:
                    return "系列的间距应该大于等于0。";

                case ChartStringId.MsgIncorrectSeriesDistanceFixed:
                    return "固定系列的间距应该大于等于0。";

                case ChartStringId.MsgIncorrectSeriesIndentFixed:
                    return "固定系列的缩进应该大于等于0。";

                case ChartStringId.MsgIncorrectPlaneDepthFixed:
                    return "固定平面的深度应该大于等于1。";

                case ChartStringId.MsgIncorrectShapeFillet:
                    return "圆角形状应大于或等于0。";

                case ChartStringId.MsgIncorrectAnnotationHeight:
                    return "该批注的高度应大于0。";

                case ChartStringId.MsgIncorrectAnnotationWidth:
                    return "该批注的宽度应大于0。";

                case ChartStringId.MsgIncorrectTextAnnotationAngle:
                    return "该批注的角度应大于或等于-360，小于或等于360。";

                case ChartStringId.MsgIncorrectRelativePositionConnectorLength:
                    return "该连接器的长度应大于或等于0且小于1000。";

                case ChartStringId.MsgIncorrectRelativePositionAngle:
                    return "角度应大于或等于-360，小于或等于360。";

                case ChartStringId.MsgIncorrectAxisCoordinateAxisValue:
                    return "轴值不能设置为null的坐标轴对象。";

                case ChartStringId.MsgIncorrectAnnotationZOrder:
                    return "顺序应大于或等于0且小于100。";

                case ChartStringId.MsgIncorrectLabelMaxWidth:
                    return "最大宽度应大于或等于0。";

                case ChartStringId.MsgNullPaneAnchorPointPane:
                    return "窗格不能设置为null窗格锚点对象。";

                case ChartStringId.MsgIncorrectPaneAnchorPointPane:
                    return "不能设置窗格锚点的窗格中，因为指定的窗格不是默认的，不包含在图表窗格集合中。";

                case ChartStringId.MsgNullAxisXCoordinateAxis:
                    return "轴不能被设置为null的X坐标轴的对象。";

                case ChartStringId.MsgIncorrectAxisXCoordinateAxis:
                    return "不能设置X坐标轴的轴，因为指定的轴是不是主要的，而不是在图表的二级X坐标集合中。";

                case ChartStringId.MsgNullAxisYCoordinateAxis:
                    return "轴不能设置为null Y坐标轴的对象。";

                case ChartStringId.MsgIncorrectAxisYCoordinateAxis:
                    return "不能设置Y坐标轴的轴，因为指定的轴是不是主要的，而不是在图中的二级y坐标集合中。";

                case ChartStringId.MsgNullAnchorPointSeriesPoint:
                    return "系列点不能设置为null窗格锚点对象。";

                case ChartStringId.MsgIncorrectAnchorPointSeriesPoint:
                    return "不能设置系列点，因为它应该属于一个系列，该系列应在图表的集合中。";

                case ChartStringId.MsgIncorrectFreePositionDockTarget:
                    return "不正确的值被指定。一个停放的目标只能是一个窗格，或null（指图表控件本身）。";

                case ChartStringId.MsgEmptyArgument:
                    return "参数不能为空。";

                case ChartStringId.MsgIncorrectImageFormat:
                    return "不可能按指定的图象格式输出表格。";

                case ChartStringId.MsgIncorrectValueDataMemberCount:
                    return "需要为当前系列视图指定{0}值数据成员。";

                case ChartStringId.MsgPaletteEditingIsntAllowed:
                    return "编辑不允许！";

                case ChartStringId.MsgPaletteDoubleClickToEdit:
                    return "双击编辑...";

                case ChartStringId.MsgInvalidPaletteName:
                    return "不能将带有空名称(\"\")的调色板增加到调色板存储器中。请为该调色板指定另外的名称。";

                case ChartStringId.MsgInvalidScaleType:
                    return "按标度的内部表示方法转换的指定值与当前的标度类型不兼容。";

                case ChartStringId.MsgIncorrectTransformationMatrix:
                    return "不正确的变换矩阵。";

                case ChartStringId.MsgIncorrectPerspectiveAngle:
                    return "透视角应该大于等于0且小于180。";

                case ChartStringId.MsgIncorrectPieDepth:
                    return "饼图的深度应该大于等于0且小于等于100，因为它的值是按百分数度量的。";

                case ChartStringId.MsgIncorrectGridSpacing:
                    return "删格间距应该大于0。";

                case ChartStringId.MsgIncompatibleArgumentScaleType:
                    return "{0}参数的标度类型和{1}系列视图不兼容。";

                case ChartStringId.MsgIncompatibleValueScaleType:
                    return "{0}值标度类型和{1}系列视图不兼容。";

                case ChartStringId.MsgIncompatibleSummaryFunction:
                    return "{0} 的总计函数不兼容缩放 {1}。";

                case ChartStringId.MsgIncorrectConstantLineAxisValue:
                    return "实线对象的轴值不能设置为空。";

                case ChartStringId.MsgIncorrectCustomAxisLabelAxisValue:
                    return "CustomAxisLabel对象，其轴值不能设置为空。";

                case ChartStringId.Msg3DRotationToolTip:
                    return "使用Ctrl键和鼠标左键\r\n来旋转图表。";

                case ChartStringId.Msg3DScrollingToolTip:
                    return "使用Ctrl键和鼠标中键（滚轮）\r\n来滚动图表。";

                case ChartStringId.Msg2DScrollingToolTip:
                    return "使用Ctrl键和鼠标中键（滚轮）\r\n来滚动图表。";

                case ChartStringId.Msg2DPieExplodingToolTip:
                    return "使用Ctrl键和鼠标左键\r\n来展开或收起扇形图。";

                case ChartStringId.MsgIncorrectTaskLinkMinIndent:
                    return "任务连接的最小缩进应该大于等于0。";

                case ChartStringId.MsgIncorrectArrowWidth:
                    return "箭头的宽度应该总为大于0的奇数。";

                case ChartStringId.MsgIncorrectArrowHeight:
                    return "箭头的高度应该大于0。";

                case ChartStringId.MsgInvalidZeroAxisAlignment:
                    return "对于二级轴，对齐不能设置为对齐.Zero。";

                case ChartStringId.MsgNullSeriesViewAxisX:
                    return "系列视图的X轴不能设置为空。";

                case ChartStringId.MsgNullSeriesViewAxisY:
                    return "系列视图的Y轴不能设置为空。";

                case ChartStringId.MsgNullSeriesViewPane:
                    return "级联视图的窗格不能设为空。";

                case ChartStringId.MsgIncorrectSeriesViewPane:
                    return "不能设置级联的窗格中，因为指定的窗格不是默认的，不包含在图表窗格集合中。";

                case ChartStringId.MsgIncorrectSeriesViewAxisX:
                    return "不能设置级联的X轴，因为指定的轴不是图的X主轴，或者根本不是主轴。";

                case ChartStringId.MsgIncorrectSeriesViewAxisY:
                    return "不能设置级联的Y轴，因为指定的轴不是图的Y主轴，或者根本不是主轴。";

                case ChartStringId.MsgIncorrectParentSeriesPointOwner:
                    return "父系列的所有者不能为空且必须为此系列类型。";

                case ChartStringId.MsgSeriesViewNotSupportRelations:
                    return "系列视图不支持关系。";

                case ChartStringId.MsgIncorrectChildSeriesPointOwner:
                    return "子系列点的所有者不能为空，且必须是此系列类型。";

                case ChartStringId.MsgIncorrectChildSeriesPointID:
                    return "子系列点的ID必须为正或者等于0。";

                case ChartStringId.MsgIncorrectSeriesOfParentAndChildPoints:
                    return "父点和子点必须属于同一系列。";

                case ChartStringId.MsgSelfRelatedSeriesPoint:
                    return "系列点不能和它自身有关联。";

                case ChartStringId.MsgSeriesPointRelationAlreadyExists:
                    return "系列点的关系采集已经包含此关系。";

                case ChartStringId.MsgChildSeriesPointNotExist:
                    return "ID等于{0}的子系列点不存在。";

                case ChartStringId.MsgRelationChildPointIDNotUnique:
                    return "关系的ChildPointID必须是独有的。";

                case ChartStringId.MsgSeriesPointIDNotUnique:
                    return "系列点的ID必须是独有的。";

                case ChartStringId.MsgIncorrectFont:
                    return "字体不能为空";

                case ChartStringId.MsgIncorrectScrollBarThickness:
                    return "滚动条的厚度应该大于等于3且小于等于25。";

                case ChartStringId.MsgIncorrectZoomPercent:
                    return "缩放百分数应该大于0且小于等于{0}。";

                case ChartStringId.MsgIncorrectHorizontalScrollPercent:
                    return "水平滚动百分比应该大于等于-{0}且小于等于{0}。";

                case ChartStringId.MsgIncorrectVerticalScrollPercent:
                    return "垂直滚动百分数应该大于等于-{0}且小于等于{0}。";

                case ChartStringId.MsgIncorrectAnchorPoint:
                    return "定位点不能为空。";

                case ChartStringId.MsgIncorrectShapePosition:
                    return "形状位置不能为空。";

                case ChartStringId.MsgIncorrectPath:
                    return "指定的路径不能恢复:{0}。";

                case ChartStringId.MsgRegisterPageInUnregisterGroup:
                    return "页面不能被注册在未注册组";

                case ChartStringId.MsgUnregisterPageError:
                    return "这个页已经取消注册。";

                case ChartStringId.MsgUnregisterGroupError:
                    return "这个组已经取消注册。";

                case ChartStringId.MsgWizardAbsractPageType:
                    return "{0}是抽象的，所以该类型的对象不能有实例且不能增加为导向页面。";

                case ChartStringId.MsgWizardIncorrectBasePageType:
                    return "{0}必须从{1}类继承。";

                case ChartStringId.MsgWizardNonUniquePageType:
                    return "类型{0}的页面已经注册。对于一个特定的类型只能有一个页面。";

                case ChartStringId.MsgWizardNonUniqueGroupName:
                    return "名称为{0}的组已经注册。";

                case ChartStringId.MsgOrderArrayLengthMismatch:
                    return "指令数组的长度和注册元素的总数量不相等。";

                case ChartStringId.MsgOrderUnregisteredElementFound:
                    return "发现没有注册的元素。";

                case ChartStringId.MsgOrderRepeatedElementFound:
                    return "同一元素在指令数组中已重复多次。";

                case ChartStringId.MsgNotChartControl:
                    return "指定对象不是一个图表控件。";

                case ChartStringId.MsgNotBelongingChart:
                    return "这个控件不包含指定图表。";

                case ChartStringId.MsgInitializeChartNotFound:
                    return "没有发现图表控件，或者是这个控件上有几个图表。解决这个问题，你需要使用向导页。运行初始化页面并手动为控件指定图表。";

                case ChartStringId.MsgAddPresentViewType:
                    return "指定的视类型在采集中已经存在。";

                case ChartStringId.MsgAddLastViewType:
                    return "在此采集中不能添加任何视类型，因为在向导中至少需要一个视类型是可用的。";

                case ChartStringId.MsgCalcHitInfoNotSupported:
                    return "3维图表类型不支持点击测试。这种方法只用于2维图表类型。";

                case ChartStringId.MsgIncorrectAppearanceName:
                    return "图表不包含名称为{0}的外观。";

                case ChartStringId.MsgIncompatibleByViewType:
                    return "视图类型";

                case ChartStringId.MsgIncompatibleByArgumentScaleType:
                    return "参数标度类型";

                case ChartStringId.MsgIncompatibleByValueScaleType:
                    return "值标度类型";

                case ChartStringId.MsgInvalidExplodedSeriesPoint:
                    return "这些指定的系列点不属于当前饼系列视图的系列点采集，所以它们不能加到爆破分散点的采集当中。";

                case ChartStringId.MsgInvalidExplodedModeAdd:
                    return "因为当前的饼系列视图显示了用系列模板创建的系列，所以指定的系列点不能加到爆破分散点的采集当中。你需要使用另外的爆破分散模式来替代。";

                case ChartStringId.MsgInvalidExplodedModeRemove:
                    return "因为当前的饼系列视图显示了用系列模板创建的系列，所以指定的系列点不能从爆破分散点的采集当中移除。你需要使用另外的爆破分散模式来替代。";

                case ChartStringId.MsgIncorrectExplodedDistancePercentage:
                    return "爆破分散的间距其百分数值应该大于0。";

                case ChartStringId.MsgIncorrectPaletteBaseColorNumber:
                    return "调色板的基础色数量应该大于等于0且小于等于其总颜色数量。";

                case ChartStringId.MsgDenyChangeSeriesPointCollection:
                    return "不能人为改变系列点的集合，因为图表和数据是绑定的。";

                case ChartStringId.MsgDenyChangeSeriesPointAgument:
                    return "不能人为改变系列点的参数，因为图表和数据是绑定的。";

                case ChartStringId.MsgDenyChangeSeriesPointValue:
                    return "不能人为改变系列点的值，因为图表和数据是绑定的。";

                case ChartStringId.MsgIncorrectStartAngle:
                    return "起始角的值应该大于等于-360度且小于等于360度。";

                case ChartStringId.MsgPolarAxisXRangeChanged:
                    return "极地X轴的范围不能改变。";

                case ChartStringId.MsgPolarAxisXGridSpacingChanged:
                    return "极地X轴的栅格间隔不能改变。";

                case ChartStringId.MsgPolarAxisXLogarithmic:
                    return "极坐标X轴不支持对数模式";

                case ChartStringId.MsgIncorrectPieArgumentScaleType:
                    return "不能指定{0}参数的标度类型，因为当前的爆破分散点过滤器与它不匹配。";

                case ChartStringId.MsgIncorrectDoughnutHolePercent:
                    return "圆环图孔的百分比应该大于等于0，小于等于100。";

                case ChartStringId.MsgIncorrectFunnelHolePercent:
                    return "漏斗洞比例应大于或等于0，小于或等于100。";

                case ChartStringId.MsgIncorrectLineTensionPercent:
                    return "线张力应大于等于0，小于等于100。";

                case ChartStringId.MsgEmptyChart:
                    return "图表中不含需要展现的可见级联。\n尝试增加新级联，或者确保\n至少一个级联是可见的。";

                case ChartStringId.MsgNoPanes:
                    return "图表中不含可见的窗格。\n试着设置图表的Diagram.DefaultPane.Visible属性为True，\n或者显示其他的图表";

                case ChartStringId.MsgChartLoadingException:
                    return "无法打开指定的XML文件，\n可能文件类型不支持，或者文件已损坏。";

                case ChartStringId.MsgIncorrectPaneWeight:
                    return "窗格的粗细度应大于0";

                case ChartStringId.MsgIncorrectPaneDistance:
                    return "窗格间距应大于等于0";

                case ChartStringId.MsgEmptyPaneTextForVerticalLayout:
                    return "指定窗格(Pane)到Series.View.Pane属性值，在窗格显示级联";

                case ChartStringId.MsgEmptyPaneTextForHorizontalLayout:
                    return "指定窗格(Pane)到Series.View.Pane属性值，在窗格显示级联";

                case ChartStringId.MsgInvalidPaneSizeInPixels:
                    return "窗格的大小像素应大于等于0";

                case ChartStringId.MsgIncorrectTopNCount:
                    return "top N的值应大于0";

                case ChartStringId.MsgIncorrectTopNThresholdValue:
                    return "top N的界限值应大于0";

                case ChartStringId.MsgIncorrectTopNThresholdPercent:
                    return "top N的界限百分比应大于0小于100";

                case ChartStringId.MsgInvalidPane:
                    return "指定的窗格要么不属于一个图表，或不显示当前轴的能见度应该改变。";

                case ChartStringId.MsgIncorrectSummaryFunction:
                    return "指定的汇总格式不正确";

                case ChartStringId.MsgNullFinancialIndicatorArgument:
                    return "不能设置为空";

                case ChartStringId.MsgUnsupportedValueLevel:
                    return "{0}等级值不支持{1}";

                case ChartStringId.MsgSummaryFunctionIsNotRegistered:
                    return "汇总函数'{0}'没有注册";

                case ChartStringId.MsgSummaryFunctionParameterIsNotSpecified:
                    return "您应该指定所有的汇总参数";

                case ChartStringId.MsgIncompatibleSummaryFunctionDimension:
                    return "The dimension of the {0} summary function isn't compatible with the {1} series view ({2} but should be {3}).";

                case ChartStringId.MsgIncorrectSummaryFunctionParametersCount:
                    return "汇总函数{0}接收参数{1}替代{2}";

                case ChartStringId.MsgWebInvalidWidthUnit:
                    return "图表宽度必须设置像素";

                case ChartStringId.MsgWebInvalidHeightUnit:
                    return "图表高度必须设置像素";

                case ChartStringId.MsgIncorrectBubbleMaxSize:
                    return "最大值必须大于最小值";

                case ChartStringId.MsgIncorrectBubbleMinSize:
                    return "最小值必须大于等于0，且小于最大值";

                case ChartStringId.MsgInvalidLogarithmicBase:
                    return "对数底必须大于1";

                case ChartStringId.MsgUnsupportedTopNOptions:
                    return "TopNOptions无法启用的这一系列，因为其ValueScaleType不是数值或其数据点超过1。";

                case ChartStringId.MsgUnsupportedResolveOverlappingMode:
                    return "指定的重叠模式是不支持目前的系列视图";

                case ChartStringId.MsgIncorrectDateTimeMeasureUnitPropertyUsing:
                    return "DateTimeMeasureUnit属性无法修改自动日期时间尺度模式";

                case ChartStringId.MsgIncorrectDateTimeGridAlignmentPropertyUsing:
                    return "DateTimeGridAlignment属性无法修改自动日期时间尺度模式";

                case ChartStringId.MsgIncorrectValueLevel:
                    return "{0}ValueLevel无效的当前回归线";

                case ChartStringId.MsgUnsupportedDateTimeScaleModeWithScrollingZooming:
                    return "在缩放和卷动时不能自动日期时间尺度模式";

                case ChartStringId.MsgUnsupportedDateTimeScaleModeForGanttDiagram:
                    return "日期时间尺度模式不支持甘特图";

                case ChartStringId.MsgIncorrectAxisRangeMinValue:
                    return "最小值不能设置为空。";

                case ChartStringId.MsgIncorrectAxisRangeMaxValue:
                    return "最大值不能设置为空。";

                case ChartStringId.MsgIncorrectAxisRangeMinValueInternal:
                    return "内部最小值不能设置为非数字和无穷大";

                case ChartStringId.MsgIncorrectAxisRangeMaxValueInternal:
                    return "内部最大0.值不能设置为非数字和无穷大";

                case ChartStringId.MsgIncorrectPropertyValue:
                    return "\"{1}\"属性值\"{0}\"不正确。";

                case ChartStringId.MsgMinMaxDifferentTypes:
                    return "最大值和最小值的类型不匹配。";

                case ChartStringId.MsgIncorrectAxisRange:
                    return "轴范围({0})的最小值应该小于它的最大值({1})。";

                case ChartStringId.MsgUnsupportedManualRangeForAutomaticDateTimeScaleMode:
                    return "如果当前时间尺度模式不是手动的，是不能设置自定义的范围";

                case ChartStringId.MsgCantSwapSeries:
                    return "不能交换自动创建和固定级联";

                case ChartStringId.MsgInvalidEdge1:
                    return "边界1值不能为空";

                case ChartStringId.MsgInvalidEdge2:
                    return "边界2值不能为空";

                case ChartStringId.MsgInvalidSizeInPixels:
                    return "像素大小应大于或等于-1和小于50";

                case ChartStringId.MsgInvalidMaxCount:
                    return "最大计数应大于0";

                case ChartStringId.MsgInvalidGradientMode:
                    return "多边形梯度模式不符合区级联视图";

                case ChartStringId.MsgIncorrectDashStyle:
                    return "DashStyle.Empty只能指定为常量行的LineStyle属性。";

                case ChartStringId.MsgAnnotationMovingToolTip:
                    return "使用Ctrl和鼠标左键移动批注。";

                case ChartStringId.MsgAnnotationResizingToolTip:
                    return "使用Ctrl和鼠标左键调整批注大小。";

                case ChartStringId.MsgAnnotationRotationToolTip:
                    return "使用Ctrl和鼠标左键旋转批注。";

                case ChartStringId.MsgAnchorPointMovingToolTip:
                    return "使用Ctrl和鼠标左键移动定位点。";

                case ChartStringId.MsgPivotGridDataSourceOptionsNotSupprotedProperty:
                    return "该PivotGridDataSourceOptions。{0}属性仅当图表的数据源是一个PivotGrid。";

                case ChartStringId.MsgDiagramToPointIncorrectValue:
                    return "指定的{0}参数类型不匹配合适的规模型，这是{1}在此轴。";

                case ChartStringId.MsgIncorrectBarDistancePropertyUsing:
                    return "不能设置条形图距离属性，除非该系列添加到图表的集合。";

                case ChartStringId.MsgIncorrectBarDistanceFixedPropertyUsing:
                    return "不能设置条形图定距属性，除非该系列添加到图表的集合。";

                case ChartStringId.MsgIncorrectEqualBarWidthPropertyUsing:
                    return "不能设置相等条形图宽度属性，除非该系列添加到图表的集合。";

                case ChartStringId.MsgFileNotFound:
                    return "文件'{0}'找不到。";

                case ChartStringId.MsgCantImportHolidays:
                    return "无法从'{0}'文件导入节假日。";

                case ChartStringId.MsgIncorrectIndicator:
                    return "你可以只添加到这个集合对象的指标。";

                case ChartStringId.MsgIncorrectBarSeriesLabelPosition:
                    return "条形图系列标签Position.Top值不支持这个系列视图类型。";

                case ChartStringId.MsgIncorrectBarSeriesLabelIndent:
                    return "缩进量应该大于等于0。";

                case ChartStringId.MsgIncorrectPointsCount:
                    return "点计数应大于1。";

                case ChartStringId.MsgIncorrectEnvelopePercent:
                    return "信封百分比应大于0，小于或等于100。";

                case ChartStringId.MsgEmptySecondaryAxisName:
                    return "次坐标轴名不能为空。";

                case ChartStringId.VerbAbout:
                    return "关于";

                case ChartStringId.VerbAboutDescription:
                    return "在XtraCharts显示基本信息";

                case ChartStringId.VerbPopulate:
                    return "展示";

                case ChartStringId.VerbPopulateDescription:
                    return "填充图表数据源";

                case ChartStringId.VerbClearDataSource:
                    return "清除数据源";

                case ChartStringId.VerbClearDataSourceDescription:
                    return "清除图表数据源";

                case ChartStringId.VerbDataSnapshot:
                    return "数据抽点打印";

                case ChartStringId.VerbDataSnapshotDescription:
                    return "从图表范围数据源复制数据和拆分数据源。";

                case ChartStringId.VerbAnnotations:
                    return "批注...";

                case ChartStringId.VerbAnnotationsDescription:
                    return "打开批注集合编辑器。";

                case ChartStringId.VerbSeries:
                    return "系列...";

                case ChartStringId.VerbSeriesDescription:
                    return "打开编辑聚集级联";

                case ChartStringId.VerbResetLegendPointOptions:
                    return "重置图例点选项";

                case ChartStringId.VerbResetLegendPointOptionsDescription:
                    return "还原图例点选项到默认值";

                case ChartStringId.VerbEditPalettes:
                    return "编辑调色板...";

                case ChartStringId.VerbEditPalettesDescription:
                    return "打开编辑调色板。";

                case ChartStringId.VerbWizard:
                    return "向导...";

                case ChartStringId.VerbWizardDescription:
                    return "运行图表向导，允许编辑哪个图表属性。";

                case ChartStringId.VerbSaveLayout:
                    return "保存...";

                case ChartStringId.VerbSaveLayoutDescription:
                    return "保存图表到XML文件。";

                case ChartStringId.VerbLoadLayout:
                    return "载入...";

                case ChartStringId.VerbLoadLayoutDescription:
                    return "从XML文件载入图表。";

                case ChartStringId.PieIncorrectValuesText:
                    return "饼图不能描绘负数。所有的值必须大于等于0。";

                case ChartStringId.FontFormat:
                    return "{0}, {1}点, {2}";

                case ChartStringId.TrnSeriesChanged:
                    return "改变系列";

                case ChartStringId.TrnDataFiltersChanged:
                    return "改变数据过滤器";

                case ChartStringId.TrnChartTitlesChanged:
                    return "改变图表标题";

                case ChartStringId.TrnPalettesChanged:
                    return "改变调色板";

                case ChartStringId.TrnConstantLinesChanged:
                    return "改变实线";

                case ChartStringId.TrnStripsChanged:
                    return "改变标记线";

                case ChartStringId.TrnCustomAxisLabelChanged:
                    return "改变定制的轴标记";

                case ChartStringId.TrnSecondaryAxesXChanged:
                    return "改变二级X轴";

                case ChartStringId.TrnSecondaryAxesYChanged:
                    return "改变二级Y轴";

                case ChartStringId.TrnXYDiagramPanesChanged:
                    return "窗格已更改";

                case ChartStringId.TrnChartWizard:
                    return "应用图表向导设置";

                case ChartStringId.TrnSeriesDeleted:
                    return "删除系列";

                case ChartStringId.TrnChartTitleDeleted:
                    return "删除图表标题";

                case ChartStringId.TrnAnnotationDeleted:
                    return "批注已经删除";

                case ChartStringId.TrnConstantLineDeleted:
                    return "改变实线";

                case ChartStringId.TrnPaneDeleted:
                    return "窗格已删除";

                case ChartStringId.TrnSecondryAxisXDeleted:
                    return "删除二级X轴";

                case ChartStringId.TrnSecondryAxisYDeleted:
                    return "删除二级Y轴";

                case ChartStringId.TrnExplodedPoints:
                    return "改变爆破分散点";

                case ChartStringId.TrnExplodedPointsFilters:
                    return "改变爆破分散点过滤器";

                case ChartStringId.TrnLegendPointOptionsReset:
                    return "图例点选项已重置";

                case ChartStringId.TrnLoadLayout:
                    return "图表布局已载入";

                case ChartStringId.TrnSeriesTitleChanged:
                    return "级联标题已修改";

                case ChartStringId.TrnSeriesTitleDeleted:
                    return "级联标题已删除";

                case ChartStringId.TrnAxisVisibilityChanged:
                    return "坐标可见性已修改";

                case ChartStringId.TrnSummaryFunctionChanged:
                    return "总计函数已修改";

                case ChartStringId.TrnIndicatorDeleted:
                    return "指示符已删除";

                case ChartStringId.TrnIndicatorsChanged:
                    return "指示符已更改";

                case ChartStringId.TrnScaleBreaksChanged:
                    return "刻度中断已改变";

                case ChartStringId.TrnAnnotationsChanged:
                    return "批注已更改";

                case ChartStringId.TrnHolidaysChanged:
                    return "节假日已更改";

                case ChartStringId.TrnExactWorkdaysChanged:
                    return "精确的工作日已更改";

                case ChartStringId.AxisXDefaultTitle:
                    return "参数轴";

                case ChartStringId.AxisYDefaultTitle:
                    return "数值轴";

                case ChartStringId.DefaultWizardPageLabel:
                    return "向导页";

                case ChartStringId.MenuItemAdd:
                    return "增加";

                case ChartStringId.MenuItemInsert:
                    return "插入";

                case ChartStringId.MenuItemDelete:
                    return "删除";

                case ChartStringId.MenuItemClear:
                    return "清除";

                case ChartStringId.MenuItemMoveUp:
                    return "上移";

                case ChartStringId.MenuItemMoveDown:
                    return "下移";

                case ChartStringId.WizAutoCreatedSeries:
                    return "自动创建系列";

                case ChartStringId.WizSpecifyDataFilters:
                    return "点击省略符按钮...";

                case ChartStringId.WizDataFiltersDisabled:
                    return "(空)";

                case ChartStringId.WizDataFiltersEntered:
                    return "{0} 数据过滤器(s)";

                case ChartStringId.WizBackImageFileNameFilter:
                    return "图象文件(*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png|All files(*.*)|*.*";

                case ChartStringId.WizNoBackImage:
                    return "(无)";

                case ChartStringId.WizConstructionGroupName:
                    return "构造";

                case ChartStringId.WizPresentationGroupName:
                    return "效果";

                case ChartStringId.WizChartTypePageName:
                    return "图表类型";

                case ChartStringId.WizAppearancePageName:
                    return "外观";

                case ChartStringId.WizSeriesPageName:
                    return "系列";

                case ChartStringId.WizDataPageName:
                    return "数据";

                case ChartStringId.WizChartPageName:
                    return "图表";

                case ChartStringId.WizDiagramPageName:
                    return "图表";

                case ChartStringId.WizAxesPageName:
                    return "轴";

                case ChartStringId.WizAxesAlignmentNear:
                    return "近";

                case ChartStringId.WizAxesAlignmentFar:
                    return "远";

                case ChartStringId.WizAxesAlignmentZero:
                    return "零";

                case ChartStringId.WizPanesPageName:
                    return "窗格";

                case ChartStringId.WizAnnotationsPageName:
                    return "批注";

                case ChartStringId.WizSeriesViewPageName:
                    return "系列视图";

                case ChartStringId.WizSeriesLabelsPageName:
                    return "点标记";

                case ChartStringId.WizChartTitlesPageName:
                    return "图表标题";

                case ChartStringId.WizLegendPageName:
                    return "图例";

                case ChartStringId.WizSeriesPointPageName:
                    return "级联点";

                case ChartStringId.WizSeriesDataBindingPageName:
                    return "级联绑定";

                case ChartStringId.WizPivotGridDataSourcePageName:
                    return "数据透视表网格数据源";

                case ChartStringId.WizChartTypePageDescription:
                    return "选择您需要使用的图表类型。如果要按图表分组过滤图表类型，请选择下拉列表中的选项。";

                case ChartStringId.WizAppearancePageDescription:
                    return "为颜色系列或者它们的数据点选择调色板。同时选择基于当前调色板的图表外观的指定风格。";

                case ChartStringId.WizSeriesPageDescription:
                    return "创建需要在图表显示的级联，并自定义它们的主要属性。";

                case ChartStringId.WizDataPageDescription:
                    return "使用[级联点]标签页来手动输入数据点。或者使用其他标签页来绑定数据源到每个级联或从数据源到自动创建级联。";

                case ChartStringId.WizChartPageDescription:
                    return "自定义图表属性。";

                case ChartStringId.WizDiagramPageDescription:
                    return "定制图表属性。";

                case ChartStringId.WizAxesPageDescription:
                    return "自定义图表的X、Y轴。注意图表预览时你可选定需要修改的轴。";

                case ChartStringId.WizPanesPageDescription:
                    return "自定义图表窗格。注意图表预览时你可选定需要修改的窗格。";

                case ChartStringId.WizAnnotationsPageDescription:
                    return "创建和定制批注固定到一个图表，窗格或系列点。请注意，您可以选择通过单击图表中的批注预览。";

                case ChartStringId.WizSeriesViewPageDescription:
                    return "自定义选中级联的级联视图属性。注意图表预览时你可选定需要修改的轴。";

                case ChartStringId.WizSeriesLabelsPageDescription:
                    return "自定义选中级联的点标签属性。注意图表预览时你可选定需要修改的轴。";

                case ChartStringId.WizChartTitlesPageDescription:
                    return "在图表中添加增加图表标题。";

                case ChartStringId.WizLegendPageDescription:
                    return "定制图例属性。";

                case ChartStringId.WizFormTitle:
                    return "制表向导";

                case ChartStringId.WizHatchMin:
                    return "最小";

                case ChartStringId.WizHatchHorizontal:
                    return "水平";

                case ChartStringId.WizHatchVertical:
                    return "垂直";

                case ChartStringId.WizHatchForwardDiagonal:
                    return "斜线";

                case ChartStringId.WizHatchBackwardDiagonal:
                    return "反斜线";

                case ChartStringId.WizHatchMax:
                    return "最大";

                case ChartStringId.WizHatchCross:
                    return "交叉";

                case ChartStringId.WizHatchLargeGrid:
                    return "大网格";

                case ChartStringId.WizHatchDiagonalCross:
                    return "对角交叉";

                case ChartStringId.WizHatchPercent05:
                    return "5%";

                case ChartStringId.WizHatchPercent10:
                    return "10%";

                case ChartStringId.WizHatchPercent20:
                    return "20%";

                case ChartStringId.WizHatchPercent25:
                    return "25%";

                case ChartStringId.WizHatchPercent30:
                    return "30%";

                case ChartStringId.WizHatchPercent40:
                    return "40%";

                case ChartStringId.WizHatchPercent50:
                    return "50%";

                case ChartStringId.WizHatchPercent60:
                    return "60%";

                case ChartStringId.WizHatchPercent70:
                    return "70%";

                case ChartStringId.WizHatchPercent75:
                    return "75%";

                case ChartStringId.WizHatchPercent80:
                    return "80%";

                case ChartStringId.WizHatchPercent90:
                    return "90%";

                case ChartStringId.WizHatchLightDownwardDiagonal:
                    return "浅色向下对角线";

                case ChartStringId.WizHatchLightUpwardDiagonal:
                    return "浅色向上对角线";

                case ChartStringId.WizHatchDarkDownwardDiagonal:
                    return "深色向下对角线";

                case ChartStringId.WizHatchDarkUpwardDiagonal:
                    return "深色向上对角线";

                case ChartStringId.WizHatchWideDownwardDiagonal:
                    return "宽向下对角线";

                case ChartStringId.WizHatchWideUpwardDiagonal:
                    return "宽向上对角线";

                case ChartStringId.WizHatchLightVertical:
                    return "浅色垂直";

                case ChartStringId.WizHatchLightHorizontal:
                    return "浅色水平";

                case ChartStringId.WizHatchNarrowVertical:
                    return "窄垂直";

                case ChartStringId.WizHatchNarrowHorizontal:
                    return "窄水平";

                case ChartStringId.WizHatchDarkVertical:
                    return "深色垂直";

                case ChartStringId.WizHatchDarkHorizontal:
                    return "深色水平";

                case ChartStringId.WizHatchDashedDownwardDiagonal:
                    return "下对角虚线";

                case ChartStringId.WizHatchDashedUpwardDiagonal:
                    return "上对角虚线";

                case ChartStringId.WizHatchDashedHorizontal:
                    return "水平虚线";

                case ChartStringId.WizHatchDashedVertical:
                    return "垂直虚线";

                case ChartStringId.WizHatchSmallConfetti:
                    return "小屑";

                case ChartStringId.WizHatchLargeConfetti:
                    return "大屑";

                case ChartStringId.WizHatchZigZag:
                    return "锯齿形";

                case ChartStringId.WizHatchWave:
                    return "波形";

                case ChartStringId.WizHatchDiagonalBrick:
                    return "对角块";

                case ChartStringId.WizHatchHorizontalBrick:
                    return "水平块";

                case ChartStringId.WizHatchWeave:
                    return "编织";

                case ChartStringId.WizHatchPlaid:
                    return "格子花";

                case ChartStringId.WizHatchDivot:
                    return "草皮";

                case ChartStringId.WizHatchDottedGrid:
                    return "点状网格";

                case ChartStringId.WizHatchDottedDiamond:
                    return "点状菱形";

                case ChartStringId.WizHatchShingle:
                    return "瓦";

                case ChartStringId.WizHatchTrellis:
                    return "格子";

                case ChartStringId.WizHatchSphere:
                    return "半球";

                case ChartStringId.WizHatchSmallGrid:
                    return "小网格";

                case ChartStringId.WizHatchSmallCheckerBoard:
                    return "小跳棋盘";

                case ChartStringId.WizHatchLargeCheckerBoard:
                    return "大跳棋盘";

                case ChartStringId.WizHatchOutlinedDiamond:
                    return "轮廓菱形";

                case ChartStringId.WizHatchSolidDiamond:
                    return "实心菱形";

                case ChartStringId.WizDataMemberNoneString:
                    return "(无)";

                case ChartStringId.WizPositionLeftColumn:
                    return "左列";

                case ChartStringId.WizPositionLeft:
                    return "左";

                case ChartStringId.WizPositionCenter:
                    return "中间";

                case ChartStringId.WizPositionRight:
                    return "右";

                case ChartStringId.WizPositionRightColumn:
                    return "右列";

                case ChartStringId.WizGradientTopToBottom:
                    return "从上到下";

                case ChartStringId.WizGradientBottomToTop:
                    return "自下而上";

                case ChartStringId.WizGradientLeftToRight:
                    return "从左向右";

                case ChartStringId.WizGradientRightToLeft:
                    return "从右至左";

                case ChartStringId.WizGradientTopLeftToBottomRight:
                    return "左上角到右下角";

                case ChartStringId.WizGradientBottomRightToTopLeft:
                    return "右下角到左上角";

                case ChartStringId.WizGradientTopRightToBottomLeft:
                    return "右上角到左下角";

                case ChartStringId.WizGradientBottomLeftToTopRight:
                    return "左下角到右上角";

                case ChartStringId.WizGradientToCenter:
                    return "到中心";

                case ChartStringId.WizGradientFromCenter:
                    return "从中心";

                case ChartStringId.WizGradientToCenterHorizontal:
                    return "到中心水平";

                case ChartStringId.WizGradientFromCenterHorizontal:
                    return "从中心水平";

                case ChartStringId.WizGradientToCenterVertical:
                    return "到中心垂直";

                case ChartStringId.WizGradientFromCenterVertical:
                    return "从中心垂直";

                case ChartStringId.WizValueLevelValue:
                    return "值";

                case ChartStringId.WizValueLevelValue_1:
                    return "值1";

                case ChartStringId.WizValueLevelValue_2:
                    return "值2";

                case ChartStringId.WizValueLevelLow:
                    return "低";

                case ChartStringId.WizValueLevelHigh:
                    return "高";

                case ChartStringId.WizValueLevelOpen:
                    return "打开";

                case ChartStringId.WizValueLevelClose:
                    return "关闭";

                case ChartStringId.WizDateTimeMeasureUnitYear:
                    return "年";

                case ChartStringId.WizDateTimeMeasureUnitQuarter:
                    return "季度";

                case ChartStringId.WizDateTimeMeasureUnitMonth:
                    return "月";

                case ChartStringId.WizDateTimeMeasureUnitWeek:
                    return "周";

                case ChartStringId.WizDateTimeMeasureUnitDay:
                    return "天";

                case ChartStringId.WizDateTimeMeasureUnitHour:
                    return "时";

                case ChartStringId.WizDateTimeMeasureUnitMinute:
                    return "分";

                case ChartStringId.WizDateTimeMeasureUnitSecond:
                    return "秒";

                case ChartStringId.WizDateTimeMeasureUnitMillisecond:
                    return "毫秒";

                case ChartStringId.WizResolveOverlappingModeNone:
                    return "无";

                case ChartStringId.WizResolveOverlappingModeDefault:
                    return "默认";

                case ChartStringId.WizResolveOverlappingModeHideOverlapping:
                    return "隐藏重叠";

                case ChartStringId.WizResolveOverlappingModeJustifyAroundPoint:
                    return "四周对齐";

                case ChartStringId.WizResolveOverlappingModeJustifyAllAroundPoints:
                    return "全部对齐";

                case ChartStringId.WizAxisLabelResolveOverlappingModeNone:
                    return "无";

                case ChartStringId.WizAxisLabelResolveOverlappingModeHideOverlapping:
                    return "隐藏重叠";

                case ChartStringId.WizErrorMessageTitle:
                    return "制表向导";

                case ChartStringId.WizInvalidBackgroundImage:
                    return "指定的文件不是正确的图片文件，请选择另一个";

                case ChartStringId.WizScrollBarAlignmentNear:
                    return "近的";

                case ChartStringId.WizScrollBarAlignmentFar:
                    return "远的";

                case ChartStringId.WizDateTimeScaleModeManual:
                    return "手动";

                case ChartStringId.WizDateTimeScaleModeAutomaticAverage:
                    return "自动：平均";

                case ChartStringId.WizDateTimeScaleModeAutomaticIntegral:
                    return "自动：积分";

                case ChartStringId.WizScaleBreakStyleRagged:
                    return "凹凸不平";

                case ChartStringId.WizScaleBreakStyleStraight:
                    return "直的";

                case ChartStringId.WizScaleBreakStyleWaved:
                    return "波浪形";

                case ChartStringId.WizBarSeriesLabelPositionTop:
                    return "上";

                case ChartStringId.WizBarSeriesLabelPositionCenter:
                    return "居中";

                case ChartStringId.WizBarSeriesLabelPositionTopInside:
                    return "内部上方";

                case ChartStringId.WizBarSeriesLabelPositionBottomInside:
                    return "内部下方";

                case ChartStringId.WizPieSeriesLabelPositionInside:
                    return "内部";

                case ChartStringId.WizPieSeriesLabelPositionOutside:
                    return "外部";

                case ChartStringId.WizPieSeriesLabelPositionRadial:
                    return "径向";

                case ChartStringId.WizPieSeriesLabelPositionTangent:
                    return "正切";

                case ChartStringId.WizPieSeriesLabelPositionTwoColumns:
                    return "2列";

                case ChartStringId.WizBubbleLabelValueToDisplayValue:
                    return "值";

                case ChartStringId.WizBubbleLabelValueToDisplayWeight:
                    return "重量";

                case ChartStringId.WizBubbleLabelValueToDisplayValueAndWeight:
                    return "值和重量";

                case ChartStringId.WizBubbleLabelPositionCenter:
                    return "居中";

                case ChartStringId.WizBubbleLabelPositionOutside:
                    return "外部";

                case ChartStringId.WizFunnelSeriesLabelPositionCenter:
                    return "居中";

                case ChartStringId.WizFunnelSeriesLabelPositionLeft:
                    return "左";

                case ChartStringId.WizFunnelSeriesLabelPositionLeftColumn:
                    return "左列";

                case ChartStringId.WizFunnelSeriesLabelPositionRight:
                    return "右";

                case ChartStringId.WizFunnelSeriesLabelPositionRightColumn:
                    return "右列";

                case ChartStringId.WizSeriesLabelTextOrientationHorizontal:
                    return "水平";

                case ChartStringId.WizSeriesLabelTextOrientationTopToBottom:
                    return "从上到下";

                case ChartStringId.WizSeriesLabelTextOrientationBottomToTop:
                    return "从下到上";

                case ChartStringId.WizBar3DModelBox:
                    return "框";

                case ChartStringId.WizBar3DModelCylinder:
                    return "圆柱面";

                case ChartStringId.WizBar3DModelCone:
                    return "圆锥形";

                case ChartStringId.WizBar3DModelPyramid:
                    return "棱锥形";

                case ChartStringId.WizEnableScrollingTrue:
                    return "启用滚动(true)";

                case ChartStringId.WizEnableScrollingFalse:
                    return "启用滚动(false)";

                case ChartStringId.WizEnableZoomingTrue:
                    return "启用缩放(true)";

                case ChartStringId.WizEnableZoomingFalse:
                    return "启用缩放(false)";

                case ChartStringId.WizShapeKindRectangle:
                    return "矩形";

                case ChartStringId.WizShapeKindRoundedRectangle:
                    return "圆角矩形";

                case ChartStringId.WizShapeKindEllipse:
                    return "椭圆形";

                case ChartStringId.WizAnnotationConnectorStyleArrow:
                    return "箭头";

                case ChartStringId.WizAnnotationConnectorStyleLine:
                    return "线形";

                case ChartStringId.WizAnnotationConnectorStyleNone:
                    return "无";

                case ChartStringId.WizAnnotationConnectorStyleNotchedArrow:
                    return "锯齿状的箭头";

                case ChartStringId.WizAnnotationConnectorStyleTail:
                    return "尾部";

                case ChartStringId.WizStringAlignmentCenter:
                    return "居中";

                case ChartStringId.WizStringAlignmentNear:
                    return "近";

                case ChartStringId.WizStringAlignmentFar:
                    return "远";

                case ChartStringId.WizChartImageSizeModeAutoSize:
                    return "自动大小";

                case ChartStringId.WizChartImageSizeModeStretch:
                    return "伸展";

                case ChartStringId.WizChartImageSizeModeTile:
                    return "并列";

                case ChartStringId.WizChartImageSizeModeZoom:
                    return "缩放";

                case ChartStringId.WizDockCornerLeftTop:
                    return "左上";

                case ChartStringId.WizDockCornerLeftBottom:
                    return "左下";

                case ChartStringId.WizDockCornerRightTop:
                    return "右上";

                case ChartStringId.WizDockCornerRightBottom:
                    return "右下";

                case ChartStringId.WizShapePositionKindFree:
                    return "空闲";

                case ChartStringId.WizShapePositionKindRelative:
                    return "有关";

                case ChartStringId.WizAnchorPointChart:
                    return "图表";

                case ChartStringId.WizAnchorPointPane:
                    return "窗格";

                case ChartStringId.WizAnchorPointSeriesPoint:
                    return "级联点";

                case ChartStringId.WizIndentUndefined:
                    return "未定义";

                case ChartStringId.WizIndentDefault:
                    return "默认";

                case ChartStringId.SvnSideBySideBar:
                    return "柱状";

                case ChartStringId.SvnStackedBar:
                    return "柱状堆叠";

                case ChartStringId.SvnFullStackedBar:
                    return "百分之百柱状堆叠";

                case ChartStringId.SvnSideBySideStackedBar:
                    return "并排堆叠柱形图";

                case ChartStringId.SvnSideBySideFullStackedBar:
                    return "100%并排堆叠柱形图";

                case ChartStringId.SvnPie:
                    return "饼状";

                case ChartStringId.SvnFunnel:
                    return "圆锥图";

                case ChartStringId.SvnDoughnut:
                    return "圆环图";

                case ChartStringId.SvnPoint:
                    return "点状";

                case ChartStringId.SvnBubble:
                    return "泡泡图";

                case ChartStringId.SvnLine:
                    return "线形";

                case ChartStringId.SvnStackedLine:
                    return "堆积折线图";

                case ChartStringId.SvnFullStackedLine:
                    return "百分比堆积折线图";

                case ChartStringId.SvnStepLine:
                    return "阶越线形";

                case ChartStringId.SvnSpline:
                    return "样条";

                case ChartStringId.SvnScatterLine:
                    return "散点图";

                case ChartStringId.SvnSpline3D:
                    return "3D样条";

                case ChartStringId.SvnArea:
                    return "面积";

                case ChartStringId.SvnStepArea:
                    return "分段区域图";

                case ChartStringId.SvnSplineArea:
                    return "样条区域图";

                case ChartStringId.SvnStackedArea:
                    return "面积堆叠";

                case ChartStringId.SvnSplineStackedArea:
                    return "叠加样条区域图";

                case ChartStringId.SvnFullStackedArea:
                    return "百分之百面积堆叠";

                case ChartStringId.SvnSplineFullStackedArea:
                    return "100%叠加样条区域图";

                case ChartStringId.SvnRangeArea:
                    return "范围区域图";

                case ChartStringId.SvnRangeArea3D:
                    return "3D范围区域图";

                case ChartStringId.SvnSpline3DArea:
                    return "3D 样条区域图";

                case ChartStringId.SvnSplineAreaStacked3D:
                    return "3D 叠加样条区域图";

                case ChartStringId.SvnSplineAreaFullStacked3D:
                    return "100% 3D 叠加样条区域图";

                case ChartStringId.SvnStock:
                    return "股指";

                case ChartStringId.SvnCandleStick:
                    return "蜡烛线";

                case ChartStringId.SvnSideBySideRangeBar:
                    return "并排范围柱状";

                case ChartStringId.SvnOverlappedRangeBar:
                    return "范围柱状";

                case ChartStringId.SvnSideBySideGantt:
                    return "并排甘特";

                case ChartStringId.SvnOverlappedGantt:
                    return "甘特";

                case ChartStringId.SvnSideBySideBar3D:
                    return "3D 柱状图";

                case ChartStringId.SvnStackedBar3D:
                    return "3D 层叠柱状图";

                case ChartStringId.SvnFullStackedBar3D:
                    return "3D 100%层叠柱状图";

                case ChartStringId.SvnManhattanBar:
                    return "曼哈顿柱状";

                case ChartStringId.SvnSideBySideStackedBar3D:
                    return "3D 并排层叠柱状图";

                case ChartStringId.SvnSideBySideFullStackedBar3D:
                    return "3D 100%并排层叠柱状图";

                case ChartStringId.SvnPie3D:
                    return "3维饼状";

                case ChartStringId.SvnDoughnut3D:
                    return "3D 圆环图";

                case ChartStringId.SvnFunnel3D:
                    return "3D 圆锥图";

                case ChartStringId.SvnLine3D:
                    return "3维线形";

                case ChartStringId.SvnStackedLine3D:
                    return "3D堆积折线图";

                case ChartStringId.SvnFullStackedLine3D:
                    return "百分比3D堆积折线图";

                case ChartStringId.SvnStepLine3D:
                    return "3维阶越线形";

                case ChartStringId.SvnArea3D:
                    return "3维面积";

                case ChartStringId.SvnStackedArea3D:
                    return "3维面积堆叠";

                case ChartStringId.SvnFullStackedArea3D:
                    return "百分之百3维面积堆叠";

                case ChartStringId.SvnStepArea3D:
                    return "3D分段区域图";

                case ChartStringId.SvnRadarPoint:
                    return "雷达点状";

                case ChartStringId.SvnRadarLine:
                    return "雷达线形";

                case ChartStringId.SvnRadarArea:
                    return "雷达面积";

                case ChartStringId.SvnPolarPoint:
                    return "极地点状";

                case ChartStringId.SvnPolarLine:
                    return "极地线形";

                case ChartStringId.SvnPolarArea:
                    return "极地面积";

                case ChartStringId.SvnSwiftPlot:
                    return "敏捷曲线";

                case ChartStringId.IndRegressionLine:
                    return "回归线";

                case ChartStringId.IndTrendLine:
                    return "趋势线";

                case ChartStringId.IndFibonacciIndicator:
                    return "Fibonacci指示器";

                case ChartStringId.IndSimpleMovingAverage:
                    return "简单移动平均";

                case ChartStringId.IndExponentialMovingAverage:
                    return "指数平滑移动平均";

                case ChartStringId.IndWeightedMovingAverage:
                    return "加权移动平均值";

                case ChartStringId.IndTriangularMovingAverage:
                    return "三角形移动平均";

                case ChartStringId.AppDefault:
                    return "默认值";

                case ChartStringId.AppNatureColors:
                    return "颜色";

                case ChartStringId.AppPastelKit:
                    return "颜料";

                case ChartStringId.AppInAFog:
                    return "困惑";

                case ChartStringId.AppTerracottaPie:
                    return "赤褐色饼图";

                case ChartStringId.AppNorthernLights:
                    return "北极光";

                case ChartStringId.AppChameleon:
                    return "可变式";

                case ChartStringId.AppTheTrees:
                    return "树状";

                case ChartStringId.AppLight:
                    return "明亮";

                case ChartStringId.AppGray:
                    return "灰色";

                case ChartStringId.AppDark:
                    return "棕黑";

                case ChartStringId.AppDarkFlat:
                    return "平面暗色";

                case ChartStringId.PltNatureColors:
                    return "颜色";

                case ChartStringId.PltPastelKit:
                    return "颜料";

                case ChartStringId.PltInAFog:
                    return "困惑";

                case ChartStringId.PltTerracottaPie:
                    return "赤褐色饼图";

                case ChartStringId.PltNorthernLights:
                    return "北极光";

                case ChartStringId.PltChameleon:
                    return "可变式";

                case ChartStringId.PltTheTrees:
                    return "树状";

                case ChartStringId.PltMixed:
                    return "混合型";

                case ChartStringId.PltOffice:
                    return "Office";

                case ChartStringId.PltBlackAndWhite:
                    return "黑白";

                case ChartStringId.PltGrayscale:
                    return "Grayscale";

                case ChartStringId.PltApex:
                    return "顶点";

                case ChartStringId.PltAspect:
                    return "方位";

                case ChartStringId.PltCivic:
                    return "公民的";

                case ChartStringId.PltConcourse:
                    return "会流";

                case ChartStringId.PltEquity:
                    return "公平";

                case ChartStringId.PltFlow:
                    return "流程";

                case ChartStringId.PltFoundry:
                    return "铸造";

                case ChartStringId.PltMedian:
                    return "中线";

                case ChartStringId.PltMetro:
                    return "地铁";

                case ChartStringId.PltModule:
                    return "模块";

                case ChartStringId.PltOpulent:
                    return "充足";

                case ChartStringId.PltOriel:
                    return "凸窗";

                case ChartStringId.PltOrigin:
                    return "原点";

                case ChartStringId.PltPaper:
                    return "纸张";

                case ChartStringId.PltSolstice:
                    return "至点";

                case ChartStringId.PltTechnic:
                    return "技术";

                case ChartStringId.PltTrek:
                    return "旅行";

                case ChartStringId.PltUrban:
                    return "城市";

                case ChartStringId.PltVerve:
                    return "活力";

                case ChartStringId.PltIndDefault:
                    return "默认值";

                case ChartStringId.DefaultMinValue:
                    return "最小";

                case ChartStringId.DefaultMaxValue:
                    return "最大";

                case ChartStringId.IncompatibleSeriesView:
                    return "(不兼容)";

                case ChartStringId.InvisibleSeriesView:
                    return "(不可见)";

                case ChartStringId.IncompatibleSeriesHeader:
                    return "这个系列是不兼容的。";

                case ChartStringId.IncompatibleSeriesMessage:
                    return "通过 {0} 和 \"{1}\"";

                case ChartStringId.PrimaryAxisXName:
                    return "X主轴";

                case ChartStringId.PrimaryAxisYName:
                    return "Y主轴";

                case ChartStringId.IOCaption:
                    return "非法操作";

                case ChartStringId.IODeleteAxis:
                    return "主轴不能删除。如果需要隐藏它，可将其可见属性设置为假。";

                case ChartStringId.IODeleteDefaultPane:
                    return "默认的窗格不能删除";

                case ChartStringId.PrintSizeModeNone:
                    return "无 (以显示在对话框上的尺寸打印图表)";

                case ChartStringId.PrintSizeModeStretch:
                    return "伸缩 (拉伸或收缩图表使尺寸符合打印页面)";

                case ChartStringId.PrintSizeModeZoom:
                    return "缩放 (按比例调整图表使尺寸最适合打印页面)";

                case ChartStringId.StyleAllColors:
                    return "所有颜色";

                case ChartStringId.StyleColorNumber:
                    return "颜色 {0}";

                case ChartStringId.DefaultPaneName:
                    return "默认窗格";

                case ChartStringId.QuarterFormat:
                    return "四分之{0}";

                case ChartStringId.OthersArgument:
                    return "其他";

                case ChartStringId.ExplodedPointsDialogExplodedColumn:
                    return "已分解";

                case ChartStringId.ScaleTypeQualitative:
                    return "定性";

                case ChartStringId.ScaleTypeNumerical:
                    return "数字";

                case ChartStringId.ScaleTypeDateTime:
                    return "日期时间";

                case ChartStringId.FunctionNameMin:
                    return "最小";

                case ChartStringId.FunctionNameMax:
                    return "最大";

                case ChartStringId.FunctionNameSum:
                    return "总计";

                case ChartStringId.FunctionNameAverage:
                    return "平均";

                case ChartStringId.FunctionNameCount:
                    return "计数";

                case ChartStringId.FunctionArgumentName:
                    return "参数";

                case ChartStringId.TitleSummaryFunction:
                    return "汇总";

                case ChartStringId.PanesVisibilityDialogVisibleColumn:
                    return "可见";

                case ChartStringId.PanesVisibilityDialogPanesColumn:
                    return "窗格";

                case ChartStringId.FibonacciArcs:
                    return "Fibonacci弧形";

                case ChartStringId.FibonacciFans:
                    return "Fibonacci扇形";

                case ChartStringId.FibonacciRetracement:
                    return "Fibonacci逆程";

                case ChartStringId.AnnotationChartAnchorPoint:
                    return "图表定位点";

                case ChartStringId.AnnotationPaneAnchorPoint:
                    return "窗格定位点";

                case ChartStringId.AnnotationSeriesPointAnchorPoint:
                    return "级联点定位点";

                case ChartStringId.AnnotationFreePosition:
                    return "空闲位置";

                case ChartStringId.AnnotationRelativePosition:
                    return "相对位置";

                case ChartStringId.TextAnnotation:
                    return "文本批注";

                case ChartStringId.ImageAnnotation:
                    return "图像批注";

                case ChartStringId.IncorrectSeriesCollectionToolTipText:
                    return "没有级联在图表集合，至少有一个系列点系列。";

                case ChartStringId.IncorrectDiagramTypeToolTipText:
                    return "没有窗格，锚，因为图表的图表类型不支持窗格。";

                case ChartStringId.DefaultSmallChartText:
                    return "增加图表大小";

                case ChartStringId.ChartControlDockTarget:
                    return "图表控件";

                case ChartStringId.Holidays:
                    return "节假日";

                case ChartStringId.ExactWorkdays:
                    return "精确的工作日";

                case ChartStringId.Holiday:
                    return "节假日";

                case ChartStringId.Workday:
                    return "工作日";

                case ChartStringId.HolidaysImportFilter:
                    return "DevExpress Scheduler holidays files (*.xml)|*.xml|Microsoft Office Outlook holidays files (*.hol)|*.hol|Text files (*.txt)|*.txt|All files (*.*)|*.*";

                case ChartStringId.AllHolidays:
                    return "全部的工作日";

                case ChartStringId.AlternateTextPlaceholder:
                    return "{0}图表{1}";

                case ChartStringId.AlternateTextSeriesPlaceholder:
                    return "显示{0}";

                case ChartStringId.AlternateTextSeriesText:
                    return "{0}级联";

                case ChartStringId.ColumnAnnotations:
                    return "注释";

                case ChartStringId.ColumnLinks:
                    return "链接";
            }
            return base.GetLocalizedString(id);
        }

        public override string Language
        {
            get
            {
                return "简体中文";
            }
        }
    }
}


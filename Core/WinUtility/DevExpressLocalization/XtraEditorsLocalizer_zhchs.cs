namespace DevLocalization
{
    using DevExpress.XtraEditors.Controls;
    using System;

    public class XtraEditorsLocalizer_zhchs : EditResLocalizer
    {
        public override string GetLocalizedString(StringId id)
        {
            switch (id)
            {
                case StringId.None:
                    return "";

                case StringId.CaptionError:
                    return "错误";

                case StringId.InvalidValueText:
                    return "无效值";

                case StringId.CheckChecked:
                    return "校验";

                case StringId.CheckUnchecked:
                    return "非校验";

                case StringId.CheckIndeterminate:
                    return "不确定";

                case StringId.DateEditToday:
                    return "今天";

                case StringId.DateEditClear:
                    return "清除";

                case StringId.OK:
                    return "确定(&O)";

                case StringId.Cancel:
                    return "取消";

                case StringId.NavigatorFirstButtonHint:
                    return "第一个";

                case StringId.NavigatorPreviousButtonHint:
                    return "前一个";

                case StringId.NavigatorPreviousPageButtonHint:
                    return "前一页";

                case StringId.NavigatorNextButtonHint:
                    return "下一个";

                case StringId.NavigatorNextPageButtonHint:
                    return "下一页";

                case StringId.NavigatorLastButtonHint:
                    return "最后一个";

                case StringId.NavigatorAppendButtonHint:
                    return "追加";

                case StringId.NavigatorRemoveButtonHint:
                    return "删除";

                case StringId.NavigatorEditButtonHint:
                    return "编辑";

                case StringId.NavigatorEndEditButtonHint:
                    return "结束编辑";

                case StringId.NavigatorCancelEditButtonHint:
                    return "取消编辑";

                case StringId.NavigatorTextStringFormat:
                    return "记录 {0} of {1}";

                case StringId.PictureEditMenuCut:
                    return "剪切";

                case StringId.PictureEditMenuCopy:
                    return "复制";

                case StringId.PictureEditMenuPaste:
                    return "粘贴";

                case StringId.PictureEditMenuDelete:
                    return "删除";

                case StringId.PictureEditMenuLoad:
                    return "调用";

                case StringId.PictureEditMenuSave:
                    return "保存";

                case StringId.PictureEditOpenFileFilter:
                    return ";*.ico;*.位图文件 (*.bmp)|*.bmp|GIF文件 (*.gif)|*.gif|JPEG文件 (*.jpg;*.jpeg)|*.jpg;*.jpeg|Icon 文件 (*.ico)|*.ico|所有图像文件 |*.bmp;*.gif;*.jpg;*.jpeg;*.ico;*.png;*.tif|所有文件 |*.*";

                case StringId.PictureEditSaveFileFilter:
                    return "位图文件 (*.bmp)|*.bmp|GIF文件 (*.gif)|*.gif|JPEG 文件 (*.jpg)|*.jpg";

                case StringId.PictureEditOpenFileTitle:
                    return "打开";

                case StringId.PictureEditSaveFileTitle:
                    return "另存为";

                case StringId.PictureEditOpenFileError:
                    return "错误的图像格式";

                case StringId.PictureEditOpenFileErrorCaption:
                    return "打开错误";

                case StringId.PictureEditCopyImageError:
                    return "不能拷贝图像";

                case StringId.LookUpEditValueIsNull:
                    return "[编辑值为空]";

                case StringId.LookUpInvalidEditValueType:
                    return "无效的 LookUpEdit 编辑值类型。";

                case StringId.LookUpColumnDefaultName:
                    return "名称";

                case StringId.MaskBoxValidateError:
                    return "输入值不完整,是否修正?\r\n\t\r\n\t\r\n\t是 - 返回编辑器,修正该值.\r\n\t否 -保留该值.\r\n\t取消 - 重设为原来的值.\r\n\t";

                case StringId.UnknownPictureFormat:
                    return "未知的图形格式";

                case StringId.DataEmpty:
                    return "没有图像数据";

                case StringId.NotValidArrayLength:
                    return "无效的数组长度。";

                case StringId.ImagePopupEmpty:
                    return "(空)";

                case StringId.ImagePopupPicture:
                    return "(图像)";

                case StringId.ColorTabCustom:
                    return "自定义";

                case StringId.ColorTabWeb:
                    return "网页";

                case StringId.ColorTabSystem:
                    return "系统";

                case StringId.CalcButtonMC:
                    return "MC";

                case StringId.CalcButtonMR:
                    return "MR";

                case StringId.CalcButtonMS:
                    return "MS";

                case StringId.CalcButtonMx:
                    return "M+";

                case StringId.CalcButtonSqrt:
                    return "平方根";

                case StringId.CalcButtonBack:
                    return "后退";

                case StringId.CalcButtonCE:
                    return "CE";

                case StringId.CalcButtonC:
                    return "C";

                case StringId.CalcError:
                    return "计算错误";

                case StringId.TabHeaderButtonPrev:
                    return "向左滚动";

                case StringId.TabHeaderButtonNext:
                    return "向右滚动";

                case StringId.TabHeaderButtonClose:
                    return "关闭";

                case StringId.TabHeaderSelectorButton:
                    return "Show Window List";

                case StringId.XtraMessageBoxOkButtonText:
                    return "确定(&O)";

                case StringId.XtraMessageBoxCancelButtonText:
                    return "取消";

                case StringId.XtraMessageBoxYesButtonText:
                    return "是(&Y)";

                case StringId.XtraMessageBoxNoButtonText:
                    return "否(&N)";

                case StringId.XtraMessageBoxAbortButtonText:
                    return "中断(&A)";

                case StringId.XtraMessageBoxRetryButtonText:
                    return "重试(&R)";

                case StringId.XtraMessageBoxIgnoreButtonText:
                    return "忽略(&I)";

                case StringId.TextEditMenuUndo:
                    return "撤销(&U)";

                case StringId.TextEditMenuCut:
                    return "剪切(&t)";

                case StringId.TextEditMenuCopy:
                    return "复制(&C)";

                case StringId.TextEditMenuPaste:
                    return "粘贴(&P)";

                case StringId.TextEditMenuDelete:
                    return "删除(&D)";

                case StringId.TextEditMenuSelectAll:
                    return "全选(&A)";

                case StringId.FilterEditorTabText:
                    return "文本";

                case StringId.FilterEditorTabVisual:
                    return "可视";

                case StringId.FilterShowAll:
                    return "(全部显示)";

                case StringId.FilterGroupAnd:
                    return "与";

                case StringId.FilterGroupNotAnd:
                    return "非与";

                case StringId.FilterGroupNotOr:
                    return "非或";

                case StringId.FilterGroupOr:
                    return "或";

                case StringId.FilterClauseAnyOf:
                    return "所有";

                case StringId.FilterClauseBeginsWith:
                    return "以...开头";

                case StringId.FilterClauseBetween:
                    return "在...之间";

                case StringId.FilterClauseBetweenAnd:
                    return "在...与...之间";

                case StringId.FilterClauseContains:
                    return "包含...";

                case StringId.FilterClauseEndsWith:
                    return "以...结尾";

                case StringId.FilterClauseEquals:
                    return "等于";

                case StringId.FilterClauseGreater:
                    return "大于";

                case StringId.FilterClauseGreaterOrEqual:
                    return "大于等于";

                case StringId.FilterClauseIsNotNull:
                    return "不为空";

                case StringId.FilterClauseIsNull:
                    return "为空";

                case StringId.FilterClauseIsNotNullOrEmpty:
                    return "Is not blank";

                case StringId.FilterClauseIsNullOrEmpty:
                    return "Is blank";

                case StringId.FilterClauseLess:
                    return "小于";

                case StringId.FilterClauseLessOrEqual:
                    return "小于等于";

                case StringId.FilterClauseLike:
                    return "与...相符";

                case StringId.FilterClauseNoneOf:
                    return "一个也没有";

                case StringId.FilterClauseNotBetween:
                    return "不在...之间";

                case StringId.FilterClauseDoesNotContain:
                    return "不包含";

                case StringId.FilterClauseDoesNotEqual:
                    return "不等于";

                case StringId.FilterClauseNotLike:
                    return "不相符";

                case StringId.FilterEmptyEnter:
                    return "<输入值>";

                case StringId.FilterEmptyParameter:
                    return "<输入参数>";

                case StringId.FilterMenuAddNewParameter:
                    return "增加参数";

                case StringId.FilterEmptyValue:
                    return "<空>";

                case StringId.FilterMenuConditionAdd:
                    return "添加条件";

                case StringId.FilterMenuGroupAdd:
                    return "添加组";

                case StringId.FilterMenuClearAll:
                    return "清除所有";

                case StringId.FilterMenuRowRemove:
                    return "删除组";

                case StringId.FilterToolTipNodeAdd:
                    return "添加一个新的条件到组";

                case StringId.FilterToolTipNodeRemove:
                    return "删除这个条件";

                case StringId.FilterToolTipNodeAction:
                    return "操作";

                case StringId.FilterToolTipValueType:
                    return "比较值(另一个字段值)";

                case StringId.FilterToolTipElementAdd:
                    return "添加一个新的项目到列表";

                case StringId.FilterToolTipKeysAdd:
                    return "(使用插入或新增键)";

                case StringId.FilterToolTipKeysRemove:
                    return "(使用删除或削减键)";

                case StringId.ContainerAccessibleEditName:
                    return "编辑控制";

                case StringId.FilterCriteriaToStringGroupOperatorAnd:
                    return "And";

                case StringId.FilterCriteriaToStringGroupOperatorOr:
                    return "Or";

                case StringId.FilterCriteriaToStringUnaryOperatorBitwiseNot:
                    return "~";

                case StringId.FilterCriteriaToStringUnaryOperatorIsNull:
                    return "Is Null";

                case StringId.FilterCriteriaToStringUnaryOperatorMinus:
                    return "-";

                case StringId.FilterCriteriaToStringUnaryOperatorNot:
                    return "Not";

                case StringId.FilterCriteriaToStringUnaryOperatorPlus:
                    return "+";

                case StringId.FilterCriteriaToStringBinaryOperatorBitwiseAnd:
                    return "&";

                case StringId.FilterCriteriaToStringBinaryOperatorBitwiseOr:
                    return "|";

                case StringId.FilterCriteriaToStringBinaryOperatorBitwiseXor:
                    return "^";

                case StringId.FilterCriteriaToStringBinaryOperatorDivide:
                    return "/";

                case StringId.FilterCriteriaToStringBinaryOperatorEqual:
                    return "=";

                case StringId.FilterCriteriaToStringBinaryOperatorGreater:
                    return ">";

                case StringId.FilterCriteriaToStringBinaryOperatorGreaterOrEqual:
                    return ">=";

                case StringId.FilterCriteriaToStringBinaryOperatorLess:
                    return "<";

                case StringId.FilterCriteriaToStringBinaryOperatorLessOrEqual:
                    return "<=";

                case StringId.FilterCriteriaToStringBinaryOperatorLike:
                    return "Like";

                case StringId.FilterCriteriaToStringBinaryOperatorMinus:
                    return "-";

                case StringId.FilterCriteriaToStringBinaryOperatorModulo:
                    return "%";

                case StringId.FilterCriteriaToStringBinaryOperatorMultiply:
                    return "*";

                case StringId.FilterCriteriaToStringBinaryOperatorNotEqual:
                    return "<>";

                case StringId.FilterCriteriaToStringBinaryOperatorPlus:
                    return "+";

                case StringId.FilterCriteriaToStringBetween:
                    return "Between";

                case StringId.FilterCriteriaToStringIn:
                    return "In";

                case StringId.FilterCriteriaToStringIsNotNull:
                    return "Is Not Null";

                case StringId.FilterCriteriaToStringNotLike:
                    return "Not Like";

                case StringId.FilterCriteriaToStringFunctionIif:
                    return "Iif";

                case StringId.FilterCriteriaToStringFunctionIsNull:
                    return "为空";

                case StringId.FilterCriteriaToStringFunctionLen:
                    return "长度";

                case StringId.FilterCriteriaToStringFunctionLower:
                    return "小写";

                case StringId.FilterCriteriaToStringFunctionNone:
                    return "无";

                case StringId.FilterCriteriaToStringFunctionSubstring:
                    return "切割字符串";

                case StringId.FilterCriteriaToStringFunctionTrim:
                    return "去空格";

                case StringId.FilterCriteriaToStringFunctionUpper:
                    return "大写";

                case StringId.FilterCriteriaToStringFunctionIsThisYear:
                    return "今天";

                case StringId.FilterCriteriaToStringFunctionIsThisMonth:
                    return "本月";

                case StringId.FilterCriteriaToStringFunctionIsThisWeek:
                    return "本周";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeThisYear:
                    return "今年";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeThisMonth:
                    return "本月";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeLastWeek:
                    return "上周";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeThisWeek:
                    return "本周";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeYesterday:
                    return "昨天";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeToday:
                    return "今天";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeNow:
                    return "现在";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeTomorrow:
                    return "明天";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeDayAfterTomorrow:
                    return "后天";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeNextWeek:
                    return "下周";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeTwoWeeksAway:
                    return "两周";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeNextMonth:
                    return "下个月";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeNextYear:
                    return "明年";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalBeyondThisYear:
                    return "除了今年";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalLaterThisYear:
                    return "今年晚些时候";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalLaterThisMonth:
                    return "本月晚些时候";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalNextWeek:
                    return "下周";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalLaterThisWeek:
                    return "这周晚些时候";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalTomorrow:
                    return "明天";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalToday:
                    return "今天";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalYesterday:
                    return "昨天";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalEarlierThisWeek:
                    return "这周早些时候";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalLastWeek:
                    return "最后一周";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalEarlierThisMonth:
                    return "本月早些时候";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalEarlierThisYear:
                    return "今年早些时候";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalPriorThisYear:
                    return "在此之前一年";

                case StringId.FilterCriteriaToStringFunctionCustom:
                    return "自定义";

                case StringId.FilterCriteriaToStringFunctionCustomNonDeterministic:
                    return "常规不确定";

                case StringId.FilterCriteriaToStringFunctionIsNullOrEmpty:
                    return "空或空格";

                case StringId.FilterCriteriaToStringFunctionConcat:
                    return "串接";

                case StringId.FilterCriteriaToStringFunctionAscii:
                    return "Ascii";

                case StringId.FilterCriteriaToStringFunctionChar:
                    return "Char";

                case StringId.FilterCriteriaToStringFunctionToInt:
                    return "To int";

                case StringId.FilterCriteriaToStringFunctionToLong:
                    return "To long";

                case StringId.FilterCriteriaToStringFunctionToFloat:
                    return "To float";

                case StringId.FilterCriteriaToStringFunctionToDouble:
                    return "To double";

                case StringId.FilterCriteriaToStringFunctionToDecimal:
                    return "To decimal";

                case StringId.FilterCriteriaToStringFunctionToStr:
                    return "To str";

                case StringId.FilterCriteriaToStringFunctionReplace:
                    return "替换";

                case StringId.FilterCriteriaToStringFunctionReverse:
                    return "逆转";

                case StringId.FilterCriteriaToStringFunctionInsert:
                    return "插入";

                case StringId.FilterCriteriaToStringFunctionCharIndex:
                    return "字符索引";

                case StringId.FilterCriteriaToStringFunctionRemove:
                    return "移除";

                case StringId.FilterCriteriaToStringFunctionAbs:
                    return "Abs";

                case StringId.FilterCriteriaToStringFunctionSqr:
                    return "Sqr";

                case StringId.FilterCriteriaToStringFunctionCos:
                    return "Cos";

                case StringId.FilterCriteriaToStringFunctionSin:
                    return "Sin";

                case StringId.FilterCriteriaToStringFunctionAtn:
                    return "Atn";

                case StringId.FilterCriteriaToStringFunctionExp:
                    return "Exp";

                case StringId.FilterCriteriaToStringFunctionLog:
                    return "Log";

                case StringId.FilterCriteriaToStringFunctionRnd:
                    return "Rnd";

                case StringId.FilterCriteriaToStringFunctionTan:
                    return "Tan";

                case StringId.FilterCriteriaToStringFunctionPower:
                    return "Power";

                case StringId.FilterCriteriaToStringFunctionSign:
                    return "Sign";

                case StringId.FilterCriteriaToStringFunctionRound:
                    return "Round";

                case StringId.FilterCriteriaToStringFunctionCeiling:
                    return "Ceiling";

                case StringId.FilterCriteriaToStringFunctionFloor:
                    return "Floor";

                case StringId.FilterCriteriaToStringFunctionMax:
                    return "Max";

                case StringId.FilterCriteriaToStringFunctionMin:
                    return "Min";

                case StringId.FilterCriteriaToStringFunctionAcos:
                    return "Acos";

                case StringId.FilterCriteriaToStringFunctionAsin:
                    return "Asin";

                case StringId.FilterCriteriaToStringFunctionAtn2:
                    return "Atn2";

                case StringId.FilterCriteriaToStringFunctionBigMul:
                    return "Big mul";

                case StringId.FilterCriteriaToStringFunctionCosh:
                    return "Cosh";

                case StringId.FilterCriteriaToStringFunctionLog10:
                    return "Log10";

                case StringId.FilterCriteriaToStringFunctionSinh:
                    return "Sinh";

                case StringId.FilterCriteriaToStringFunctionTanh:
                    return "Tanh";

                case StringId.FilterCriteriaToStringFunctionPadLeft:
                    return "左对齐";

                case StringId.FilterCriteriaToStringFunctionPadRight:
                    return "右对齐";

                case StringId.FilterCriteriaToStringFunctionDateDiffTick:
                    return "时间差（刻度）";

                case StringId.FilterCriteriaToStringFunctionDateDiffSecond:
                    return "时间差（秒）";

                case StringId.FilterCriteriaToStringFunctionDateDiffMilliSecond:
                    return "时间差（毫秒）";

                case StringId.FilterCriteriaToStringFunctionDateDiffMinute:
                    return "时间差（分钟）";

                case StringId.FilterCriteriaToStringFunctionDateDiffHour:
                    return "时间差（小时）";

                case StringId.FilterCriteriaToStringFunctionDateDiffDay:
                    return "时间差（天）";

                case StringId.FilterCriteriaToStringFunctionDateDiffMonth:
                    return "时间差（月）";

                case StringId.FilterCriteriaToStringFunctionDateDiffYear:
                    return "时间差（年）";

                case StringId.FilterCriteriaToStringFunctionGetDate:
                    return "获取日期";

                case StringId.FilterCriteriaToStringFunctionGetMilliSecond:
                    return "获取毫秒";

                case StringId.FilterCriteriaToStringFunctionGetSecond:
                    return "获取秒";

                case StringId.FilterCriteriaToStringFunctionGetMinute:
                    return "获取分钟";

                case StringId.FilterCriteriaToStringFunctionGetHour:
                    return "获取小时";

                case StringId.FilterCriteriaToStringFunctionGetDay:
                    return "获取天";

                case StringId.FilterCriteriaToStringFunctionGetMonth:
                    return "获取月";

                case StringId.FilterCriteriaToStringFunctionGetYear:
                    return "获取年";

                case StringId.FilterCriteriaToStringFunctionGetDayOfWeek:
                    return "获取星期几";

                case StringId.FilterCriteriaToStringFunctionGetDayOfYear:
                    return "获取一年中的天";

                case StringId.FilterCriteriaToStringFunctionGetTimeOfDay:
                    return "获取时间";

                case StringId.FilterCriteriaToStringFunctionNow:
                    return "获取现在时间";

                case StringId.FilterCriteriaToStringFunctionUtcNow:
                    return "获取现在时间（UTC）";

                case StringId.FilterCriteriaToStringFunctionToday:
                    return "获取今天的日期";

                case StringId.FilterCriteriaToStringFunctionAddTimeSpan:
                    return "增加时间跨度";

                case StringId.FilterCriteriaToStringFunctionAddTicks:
                    return "增加刻度";

                case StringId.FilterCriteriaToStringFunctionAddMilliSeconds:
                    return "增加毫秒";

                case StringId.FilterCriteriaToStringFunctionAddSeconds:
                    return "增加秒";

                case StringId.FilterCriteriaToStringFunctionAddMinutes:
                    return "增加分钟";

                case StringId.FilterCriteriaToStringFunctionAddHours:
                    return "增加小时";

                case StringId.FilterCriteriaToStringFunctionAddDays:
                    return "增加天数";

                case StringId.FilterCriteriaToStringFunctionAddMonths:
                    return "增加月数";

                case StringId.FilterCriteriaToStringFunctionAddYears:
                    return "增加年数";

                case StringId.FilterCriteriaToStringFunctionStartsWith:
                    return "从这里开始";

                case StringId.FilterCriteriaToStringFunctionEndsWith:
                    return "到这里结束";

                case StringId.FilterCriteriaToStringFunctionContains:
                    return "包含";

                case StringId.FilterCriteriaInvalidExpression:
                    return "指定的表达包含无效符号(第{0}行，第{1}列)";

                case StringId.FilterCriteriaInvalidExpressionEx:
                    return "指定的表达式是无效的";

                case StringId.Apply:
                    return "应用";

                case StringId.PreviewPanelText:
                    return "预览:";

                case StringId.TransparentBackColorNotSupported:
                    return "此控件不支持透明的背景色";

                case StringId.FilterOutlookDateText:
                    return "查看全部|显示空值|过滤器的具体日期： |除了今年|今年晚些时候|本月晚些时候|下周|本星期晚些时候|明天|今天|昨天|本周|上周|本月|今年|在此之前一年";

                case StringId.FilterDateTimeConstantMenuCaption:
                    return "日期常量";

                case StringId.FilterDateTimeOperatorMenuCaption:
                    return "日期运算符";

                case StringId.FilterDateTextAlt:
                    return "查看全部|显示空值|过滤器的具体日期：|较远的|下周|今天|本周|本月|最近的|{0:yyyy}, {0:MMMM}";

                case StringId.FilterFunctionsMenuCaption:
                    return "Functions";

                case StringId.DefaultBooleanTrue:
                    return "真";

                case StringId.DefaultBooleanFalse:
                    return "假";

                case StringId.DefaultBooleanDefault:
                    return "默认";

                case StringId.ProgressExport:
                    return "导出";

                case StringId.ProgressPrinting:
                    return "打印";

                case StringId.ProgressCreateDocument:
                    return "创建文件";

                case StringId.ProgressCancel:
                    return "取消";

                case StringId.ProgressCancelPending:
                    return "取消等待";

                case StringId.ProgressLoadingData:
                    return "加载数据";

                case StringId.FilterAggregateAvg:
                    return "平均";

                case StringId.FilterAggregateCount:
                    return "计数";

                case StringId.FilterAggregateExists:
                    return "存在";

                case StringId.FilterAggregateMax:
                    return "最大值";

                case StringId.FilterAggregateMin:
                    return "最小值";

                case StringId.FilterAggregateSum:
                    return "合计";

                case StringId.FieldListName:
                    return "字段列表 ({0})";

                case StringId.RestoreLayoutDialogFileFilter:
                    return "XML 文件 (*.xml)|*.xml|所有文件|*.*";

                case StringId.SaveLayoutDialogFileFilter:
                    return "XML 文件 (*.xml)|*.xml";

                case StringId.RestoreLayoutDialogTitle:
                    return "还原布局";

                case StringId.SaveLayoutDialogTitle:
                    return "保存布局";

                case StringId.PictureEditMenuZoom:
                    return "缩放";

                case StringId.PictureEditMenuFullSize:
                    return "全屏";

                case StringId.PictureEditMenuFitImage:
                    return "图像填充";

                case StringId.PictureEditMenuZoomIn:
                    return "放大";

                case StringId.PictureEditMenuZoomOut:
                    return "缩小";

                case StringId.PictureEditMenuZoomTo:
                    return "放大到,";

                case StringId.PictureEditMenuZoomToolTip:
                    return "{0}%";
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


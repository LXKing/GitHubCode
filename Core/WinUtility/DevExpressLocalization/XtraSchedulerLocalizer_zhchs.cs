//namespace DevLocalization
//{
//    using DevExpress.XtraScheduler.Localization;
//    using System;

//    public class XtraSchedulerLocalizer_zhchs : SchedulerResLocalizer
//    {
//        public override string GetLocalizedString(SchedulerStringId id)
//        {
//            switch (id)
//            {
//                case SchedulerStringId.DefaultToolTipStringFormat_SplitAppointment:
//                    return "{0} : 步骤 {1}";

//                case SchedulerStringId.Msg_IsNotValid:
//                    return "'{0}' 不是一个有效值，对于 '{1}'来说";

//                case SchedulerStringId.Msg_InvalidDayOfWeekForDailyRecurrence:
//                    return "对于日期引用来说，是一个无效的周日数值. 只有周日.每天, 周日.周末和周日 .在当前上下文内，周日数值有效.";

//                case SchedulerStringId.Msg_InternalError:
//                    return "内部错误!";

//                case SchedulerStringId.Msg_NoMappingForObject:
//                    return "以下必需的关于对象{0}的映射没有指定";

//                case SchedulerStringId.Msg_XtraSchedulerNotAssigned:
//                    return "控件SchedulerStorage没有指定分配到控件SchedulerControl上";

//                case SchedulerStringId.Msg_InvalidTimeOfDayInterval:
//                    return "TimeOfDayInterval的无效的持续时间值";

//                case SchedulerStringId.Msg_OverflowTimeOfDayInterval:
//                    return "TimeOfDayInterval的无效值。 应该小于或者等于一天";

//                case SchedulerStringId.Msg_LoadCollectionFromXml:
//                    return "日程安排器要求以菲绑定的方式来从xml数据源中导入数据项。";

//                case SchedulerStringId.AppointmentLabel_None:
//                    return "没有";

//                case SchedulerStringId.AppointmentLabel_Important:
//                    return "重要";

//                case SchedulerStringId.AppointmentLabel_Business:
//                    return "商务";

//                case SchedulerStringId.AppointmentLabel_Personal:
//                    return "个人";

//                case SchedulerStringId.AppointmentLabel_Vacation:
//                    return "假期";

//                case SchedulerStringId.AppointmentLabel_MustAttend:
//                    return "必须出席";

//                case SchedulerStringId.AppointmentLabel_TravelRequired:
//                    return "要求旅游";

//                case SchedulerStringId.AppointmentLabel_NeedsPreparation:
//                    return "需要准备";

//                case SchedulerStringId.AppointmentLabel_Birthday:
//                    return "生日";

//                case SchedulerStringId.AppointmentLabel_Anniversary:
//                    return "周年纪念日";

//                case SchedulerStringId.AppointmentLabel_PhoneCall:
//                    return "电话";

//                case SchedulerStringId.Appointment_StartContinueText:
//                    return "From";

//                case SchedulerStringId.Appointment_EndContinueText:
//                    return "To";

//                case SchedulerStringId.Msg_InvalidEndDate:
//                    return "你输入的结束日期在起始日期之前。";

//                case SchedulerStringId.Caption_Appointment:
//                    return "{0} - 日程安排";

//                case SchedulerStringId.Caption_Event:
//                    return "{0} - 事件";

//                case SchedulerStringId.Caption_UntitledAppointment:
//                    return "无标题";

//                case SchedulerStringId.Caption_ReadOnly:
//                    return " [只读]";

//                case SchedulerStringId.Caption_WeekDaysEveryDay:
//                    return "日";

//                case SchedulerStringId.Caption_WeekDaysWeekendDays:
//                    return "周末";

//                case SchedulerStringId.Caption_WeekDaysWorkDays:
//                    return "周日";

//                case SchedulerStringId.Caption_WeekOfMonthFirst:
//                    return "第一周";

//                case SchedulerStringId.Caption_WeekOfMonthSecond:
//                    return "第二周";

//                case SchedulerStringId.Caption_WeekOfMonthThird:
//                    return "第三周";

//                case SchedulerStringId.Caption_WeekOfMonthFourth:
//                    return "第四周";

//                case SchedulerStringId.Caption_WeekOfMonthLast:
//                    return "最后一周";

//                case SchedulerStringId.Msg_InvalidDayCount:
//                    return "无效天数数值。请输入一个正整数值。";

//                case SchedulerStringId.Msg_InvalidDayCountValue:
//                    return "无效天数数值。 请输入一个正整数值。";

//                case SchedulerStringId.Msg_InvalidWeekCount:
//                    return "无效的周数数值。 请输入一个正整数值。";

//                case SchedulerStringId.Msg_InvalidWeekCountValue:
//                    return "无效的周数数值。请输入一个正整数值。";

//                case SchedulerStringId.Msg_InvalidMonthCount:
//                    return "无效的月份数值。 请输入一个正整数值。";

//                case SchedulerStringId.Msg_InvalidMonthCountValue:
//                    return "无效的月份数值。请输入一个正整数值。";

//                case SchedulerStringId.Msg_InvalidYearCount:
//                    return "无效的年份数值。请输入一个正整数值。";

//                case SchedulerStringId.Msg_InvalidYearCountValue:
//                    return "无效的年份数值。 请输入一个正整数值。";

//                case SchedulerStringId.Msg_InvalidOccurrencesCount:
//                    return "无效的发生次数。请输入一个正整数值。";

//                case SchedulerStringId.Msg_InvalidOccurrencesCountValue:
//                    return "无效的发生次数。请输入一个正整数值。";

//                case SchedulerStringId.Msg_InvalidDayNumber:
//                    return "无效的天数值。请输入从整数值从1到 {0}。";

//                case SchedulerStringId.Msg_InvalidDayNumberValue:
//                    return "无效的天数值。请输入从整数值从1到 {0}。";

//                case SchedulerStringId.Msg_WarningDayNumber:
//                    return "某些月份会少于{0}天数。对于这些月份来说，这将会发生在月份的最后一天。";

//                case SchedulerStringId.Msg_InvalidDayOfWeek:
//                    return "没有选择日。请至少选择一周之中的一天。";

//                case SchedulerStringId.Msg_WarningAppointmentDeleted:
//                    return "约会已被另一个用户删除";

//                case SchedulerStringId.MenuCmd_OpenAppointment:
//                    return "打开";

//                case SchedulerStringId.MenuCmd_PrintAppointment:
//                    return "打印";

//                case SchedulerStringId.MenuCmd_DeleteAppointment:
//                    return "删除";

//                case SchedulerStringId.MenuCmd_EditSeries:
//                    return "编辑系列";

//                case SchedulerStringId.MenuCmd_RestoreOccurrence:
//                    return "恢复默认状态";

//                case SchedulerStringId.MenuCmd_NewAppointment:
//                    return "新建日程安排";

//                case SchedulerStringId.MenuCmd_NewAllDayEvent:
//                    return "新建所有当天事件";

//                case SchedulerStringId.MenuCmd_NewRecurringAppointment:
//                    return "新建定期日程安排";

//                case SchedulerStringId.MenuCmd_NewRecurringEvent:
//                    return "新建定期事件";

//                case SchedulerStringId.MenuCmd_EditAppointmentDependency:
//                    return "编辑(&E)";

//                case SchedulerStringId.DescCmd_EditAppointmentDependency:
//                    return "编辑约会依赖";

//                case SchedulerStringId.MenuCmd_DeleteAppointmentDependency:
//                    return "删除(&D)";

//                case SchedulerStringId.DescCmd_DeleteAppointmentDependency:
//                    return "删除约会依赖";

//                case SchedulerStringId.MenuCmd_GotoThisDay:
//                    return "跳转到这一天";

//                case SchedulerStringId.MenuCmd_GotoToday:
//                    return "跳转到某一天";

//                case SchedulerStringId.DescCmd_GotoToday:
//                    return "改变日期显示当前视图到当前日期";

//                case SchedulerStringId.MenuCmd_GotoDate:
//                    return "跳转到某日期...";

//                case SchedulerStringId.MenuCmd_OtherSettings:
//                    return "其他设置...";

//                case SchedulerStringId.MenuCmd_CustomizeCurrentView:
//                    return "自定义当前视图...";

//                case SchedulerStringId.MenuCmd_CustomizeTimeRuler:
//                    return "自定义时间标尺...";

//                case SchedulerStringId.MenuCmd_5Minutes:
//                    return "5 分钟";

//                case SchedulerStringId.MenuCmd_6Minutes:
//                    return "6 分钟";

//                case SchedulerStringId.MenuCmd_10Minutes:
//                    return "10 分钟";

//                case SchedulerStringId.MenuCmd_15Minutes:
//                    return "15 分钟";

//                case SchedulerStringId.MenuCmd_20Minutes:
//                    return "20 分钟";

//                case SchedulerStringId.MenuCmd_30Minutes:
//                    return "30 分钟";

//                case SchedulerStringId.MenuCmd_60Minutes:
//                    return "60 分钟";

//                case SchedulerStringId.MenuCmd_SwitchViewMenu:
//                    return "更改视图到";

//                case SchedulerStringId.MenuCmd_SwitchToDayView:
//                    return "日视图";

//                case SchedulerStringId.MenuCmd_SwitchToWorkWeekView:
//                    return "工作周视图";

//                case SchedulerStringId.MenuCmd_SwitchToWeekView:
//                    return "周视图";

//                case SchedulerStringId.MenuCmd_SwitchToMonthView:
//                    return "月视图";

//                case SchedulerStringId.MenuCmd_SwitchToTimelineView:
//                    return "时间(&T)";

//                case SchedulerStringId.MenuCmd_SwitchToGroupByNone:
//                    return "按无分组(&G)";

//                case SchedulerStringId.MenuCmd_SwitchToGroupByResource:
//                    return "按资源分组(&G)";

//                case SchedulerStringId.MenuCmd_SwitchToGroupByDate:
//                    return "按日期分组(&G)";

//                case SchedulerStringId.MenuCmd_SwitchToGanttView:
//                    return "甘特图视图(&G)";

//                case SchedulerStringId.MenuCmd_TimeScalesMenu:
//                    return "时间刻度(&T)";

//                case SchedulerStringId.MenuCmd_TimeScaleCaptionsMenu:
//                    return "时间刻度标题(&C)";

//                case SchedulerStringId.MenuCmd_TimeScaleHour:
//                    return "时(&H)";

//                case SchedulerStringId.MenuCmd_TimeScaleDay:
//                    return "日(&D)";

//                case SchedulerStringId.MenuCmd_TimeScaleWeek:
//                    return "周(&W)";

//                case SchedulerStringId.MenuCmd_TimeScaleMonth:
//                    return "月(&M)";

//                case SchedulerStringId.MenuCmd_TimeScaleQuarter:
//                    return "一刻钟(&Q)";

//                case SchedulerStringId.MenuCmd_TimeScaleYear:
//                    return "年(&Y)";

//                case SchedulerStringId.MenuCmd_ShowTimeAs:
//                    return "显示时间为";

//                case SchedulerStringId.MenuCmd_Free:
//                    return "空闲";

//                case SchedulerStringId.MenuCmd_Busy:
//                    return "忙碌";

//                case SchedulerStringId.MenuCmd_Tentative:
//                    return "暂时";

//                case SchedulerStringId.MenuCmd_OutOfOffice:
//                    return "不在办公室";

//                case SchedulerStringId.MenuCmd_LabelAs:
//                    return "标记为";

//                case SchedulerStringId.MenuCmd_AppointmentLabelNone:
//                    return "没有";

//                case SchedulerStringId.MenuCmd_AppointmentLabelImportant:
//                    return "重要";

//                case SchedulerStringId.MenuCmd_AppointmentLabelBusiness:
//                    return "商务";

//                case SchedulerStringId.MenuCmd_AppointmentLabelPersonal:
//                    return "个人";

//                case SchedulerStringId.MenuCmd_AppointmentLabelVacation:
//                    return "假期";

//                case SchedulerStringId.MenuCmd_AppointmentLabelMustAttend:
//                    return "必须 &出席";

//                case SchedulerStringId.MenuCmd_AppointmentLabelTravelRequired:
//                    return "要求旅游";

//                case SchedulerStringId.MenuCmd_AppointmentLabelNeedsPreparation:
//                    return "需要准备";

//                case SchedulerStringId.MenuCmd_AppointmentLabelBirthday:
//                    return "生日";

//                case SchedulerStringId.MenuCmd_AppointmentLabelAnniversary:
//                    return "周年纪念日";

//                case SchedulerStringId.MenuCmd_AppointmentLabelPhoneCall:
//                    return "电话";

//                case SchedulerStringId.MenuCmd_AppointmentMove:
//                    return "移动";

//                case SchedulerStringId.MenuCmd_AppointmentCopy:
//                    return "拷贝";

//                case SchedulerStringId.MenuCmd_AppointmentCancel:
//                    return "取消";

//                case SchedulerStringId.Caption_5Minutes:
//                    return "5 分钟";

//                case SchedulerStringId.Caption_6Minutes:
//                    return "6 分钟";

//                case SchedulerStringId.Caption_10Minutes:
//                    return "10 分钟";

//                case SchedulerStringId.Caption_15Minutes:
//                    return "15 分钟";

//                case SchedulerStringId.Caption_20Minutes:
//                    return "20 分钟";

//                case SchedulerStringId.Caption_30Minutes:
//                    return "30 分钟";

//                case SchedulerStringId.Caption_60Minutes:
//                    return "60 分钟";

//                case SchedulerStringId.Caption_Free:
//                    return "空闲";

//                case SchedulerStringId.Caption_Busy:
//                    return "忙碌";

//                case SchedulerStringId.Caption_Tentative:
//                    return "暂时";

//                case SchedulerStringId.Caption_OutOfOffice:
//                    return "不在办公室";

//                case SchedulerStringId.Caption_NoneReminder:
//                    return "None";

//                case SchedulerStringId.ViewDisplayName_Day:
//                    return "日历";

//                case SchedulerStringId.ViewDisplayName_WorkDays:
//                    return "工作周历";

//                case SchedulerStringId.ViewDisplayName_Week:
//                    return "周历";

//                case SchedulerStringId.ViewDisplayName_Month:
//                    return "月历";

//                case SchedulerStringId.ViewDisplayName_Timeline:
//                    return "时间日历";

//                case SchedulerStringId.ViewDisplayName_Gantt:
//                    return "甘特图视图";

//                case SchedulerStringId.ViewShortDisplayName_Day:
//                    return "日";

//                case SchedulerStringId.ViewShortDisplayName_WorkDays:
//                    return "工作周";

//                case SchedulerStringId.ViewShortDisplayName_Week:
//                    return "周";

//                case SchedulerStringId.ViewShortDisplayName_Month:
//                    return "月";

//                case SchedulerStringId.ViewShortDisplayName_Timeline:
//                    return "时间";

//                case SchedulerStringId.ViewShortDisplayName_Gantt:
//                    return "甘特图";

//                case SchedulerStringId.TimeScaleDisplayName_Hour:
//                    return "时";

//                case SchedulerStringId.TimeScaleDisplayName_Day:
//                    return "日";

//                case SchedulerStringId.TimeScaleDisplayName_Week:
//                    return "周";

//                case SchedulerStringId.TimeScaleDisplayName_Month:
//                    return "月";

//                case SchedulerStringId.TimeScaleDisplayName_Quarter:
//                    return "一刻钟";

//                case SchedulerStringId.TimeScaleDisplayName_Year:
//                    return "年";

//                case SchedulerStringId.Abbr_MinutesShort1:
//                    return "分";

//                case SchedulerStringId.Abbr_MinutesShort2:
//                    return "分";

//                case SchedulerStringId.Abbr_Minute:
//                    return "分钟";

//                case SchedulerStringId.Abbr_Minutes:
//                    return "分钟";

//                case SchedulerStringId.Abbr_HoursShort:
//                    return "小时";

//                case SchedulerStringId.Abbr_Hour:
//                    return "小时";

//                case SchedulerStringId.Abbr_Hours:
//                    return "小时";

//                case SchedulerStringId.Abbr_DaysShort:
//                    return "日";

//                case SchedulerStringId.Abbr_Day:
//                    return "日";

//                case SchedulerStringId.Abbr_Days:
//                    return "日";

//                case SchedulerStringId.Abbr_WeeksShort:
//                    return "周";

//                case SchedulerStringId.Abbr_Week:
//                    return "周";

//                case SchedulerStringId.Abbr_Weeks:
//                    return "周";

//                case SchedulerStringId.Abbr_Month:
//                    return "月";

//                case SchedulerStringId.Abbr_Months:
//                    return "月";

//                case SchedulerStringId.Abbr_Year:
//                    return "年";

//                case SchedulerStringId.Abbr_Years:
//                    return "年";

//                case SchedulerStringId.Caption_Reminder:
//                    return "{0} 提醒者";

//                case SchedulerStringId.Caption_Reminders:
//                    return "{0} 提醒者";

//                case SchedulerStringId.Caption_StartTime:
//                    return "开始时间: {0}";

//                case SchedulerStringId.Caption_NAppointmentsAreSelected:
//                    return "{0} 日程安排已经选定";

//                case SchedulerStringId.Format_TimeBeforeStart:
//                    return "{0} 开始之前";

//                case SchedulerStringId.Msg_Conflict:
//                    return "编辑的日程安排与其他日程安排相冲突。";

//                case SchedulerStringId.Msg_InvalidAppointmentDuration:
//                    return "无效的间隔时长值。请输入一个正数值。";

//                case SchedulerStringId.Msg_InvalidReminderTimeBeforeStart:
//                    return "无效的事件提醒时间值。 请输入一个正数值。";

//                case SchedulerStringId.TextDuration_FromTo:
//                    return "从{0} 到 {1}";

//                case SchedulerStringId.TextDuration_FromForDays:
//                    return "从 {0} 开始延续 {1} ";

//                case SchedulerStringId.TextDuration_FromForDaysHours:
//                    return "从 {0} 开始延续 {1} {2}";

//                case SchedulerStringId.TextDuration_FromForDaysMinutes:
//                    return "从 {0} 开始延续 {1} {3}";

//                case SchedulerStringId.TextDuration_FromForDaysHoursMinutes:
//                    return "从 {0} 开始延续 {1} {2} {3}";

//                case SchedulerStringId.TextDuration_ForPattern:
//                    return "{0} {1}";

//                case SchedulerStringId.TextDailyPatternString_EveryDay:
//                    return "每 {0} {1}";

//                case SchedulerStringId.TextDailyPatternString_EveryDays:
//                    return "每 {0} {1} {2}";

//                case SchedulerStringId.TextDailyPatternString_EveryWeekDay:
//                    return "每个周日 {0}";

//                case SchedulerStringId.TextDailyPatternString_EveryWeekend:
//                    return "每个周末 {0}";

//                case SchedulerStringId.TextWeekly_0Day:
//                    return "未指定星期几";

//                case SchedulerStringId.TextWeekly_1Day:
//                    return "{0}";

//                case SchedulerStringId.TextWeekly_2Day:
//                    return "{0} 和 {1}";

//                case SchedulerStringId.TextWeekly_3Day:
//                    return "{0}, {1}, 和 {2}";

//                case SchedulerStringId.TextWeekly_4Day:
//                    return "{0}, {1}, {2}, 和 {3}";

//                case SchedulerStringId.TextWeekly_5Day:
//                    return "{0}, {1}, {2}, {3}, 和 {4}";

//                case SchedulerStringId.TextWeekly_6Day:
//                    return "{0}, {1}, {2}, {3}, {4}, 和 {5}";

//                case SchedulerStringId.TextWeekly_7Day:
//                    return "{0}, {1}, {2}, {3}, {4}, {5}, 和 {6}";

//                case SchedulerStringId.TextWeeklyPatternString_EveryWeek:
//                    return "每 {2} {3}";

//                case SchedulerStringId.TextWeeklyPatternString_EveryWeeks:
//                    return "每 {0} {1} 在 {2} {3}";

//                case SchedulerStringId.TextMonthlyPatternString_SubPattern:
//                    return "每 {0} {1} {2}";

//                case SchedulerStringId.TextMonthlyPatternString1:
//                    return "day {3} {0}";

//                case SchedulerStringId.TextMonthlyPatternString2:
//                    return "the {1} {2} {0}";

//                case SchedulerStringId.TextYearlyPattern_YearString1:
//                    return "每 {0} {1} {4}";

//                case SchedulerStringId.TextYearlyPattern_YearString2:
//                    return "第 {0} {1} of {2} {5}";

//                case SchedulerStringId.TextYearlyPattern_YearsString1:
//                    return "每{2} {3} {4} 的 {0} {1}";

//                case SchedulerStringId.TextYearlyPattern_YearsString2:
//                    return "第 {0} {1} of {2} 每 {3} {4} {5}";

//                case SchedulerStringId.Caption_AllDay:
//                    return "全天";

//                case SchedulerStringId.Caption_PleaseSeeAbove:
//                    return "请看上面";

//                case SchedulerStringId.Caption_RecurrenceSubject:
//                    return "主题:";

//                case SchedulerStringId.Caption_RecurrenceLocation:
//                    return "地点:";

//                case SchedulerStringId.Caption_RecurrenceStartTime:
//                    return "起始时间:";

//                case SchedulerStringId.Caption_RecurrenceEndTime:
//                    return "结束时间:";

//                case SchedulerStringId.Caption_RecurrenceShowTimeAs:
//                    return "显示时间为:";

//                case SchedulerStringId.Caption_Recurrence:
//                    return "循环:";

//                case SchedulerStringId.Caption_RecurrencePattern:
//                    return "循环模式:";

//                case SchedulerStringId.Caption_NoneRecurrence:
//                    return "(无)";

//                case SchedulerStringId.MemoPrintDateFormat:
//                    return "{0} {1} {2}";

//                case SchedulerStringId.Caption_EmptyResource:
//                    return "任何";

//                case SchedulerStringId.Caption_DailyPrintStyle:
//                    return "日风格";

//                case SchedulerStringId.Caption_WeeklyPrintStyle:
//                    return "周风格";

//                case SchedulerStringId.Caption_MonthlyPrintStyle:
//                    return "月风格";

//                case SchedulerStringId.Caption_TrifoldPrintStyle:
//                    return "三重风格 Style";

//                case SchedulerStringId.Caption_CalendarDetailsPrintStyle:
//                    return "日历风格";

//                case SchedulerStringId.Caption_MemoPrintStyle:
//                    return "备忘录风格";

//                case SchedulerStringId.Caption_ColorConverterFullColor:
//                    return "全色";

//                case SchedulerStringId.Caption_ColorConverterGrayScale:
//                    return "灰度色标";

//                case SchedulerStringId.Caption_ColorConverterBlackAndWhite:
//                    return "单色";

//                case SchedulerStringId.Caption_ResourceNone:
//                    return "(无)";

//                case SchedulerStringId.Caption_ResourceAll:
//                    return "(所有)";

//                case SchedulerStringId.PrintPageSetupFormatTabControlCustomizeShading:
//                    return "<自定义...>";

//                case SchedulerStringId.PrintPageSetupFormatTabControlSizeAndFontName:
//                    return "{0} pt. {1}";

//                case SchedulerStringId.PrintRangeControlInvalidDate:
//                    return "结束日期必须大于或者等于开始日期。";

//                case SchedulerStringId.PrintCalendarDetailsControlDayPeriod:
//                    return "日";

//                case SchedulerStringId.PrintCalendarDetailsControlWeekPeriod:
//                    return "周";

//                case SchedulerStringId.PrintCalendarDetailsControlMonthPeriod:
//                    return "月";

//                case SchedulerStringId.PrintMonthlyOptControlOnePagePerMonth:
//                    return "1 页/月";

//                case SchedulerStringId.PrintMonthlyOptControlTwoPagesPerMonth:
//                    return "2 页/月";

//                case SchedulerStringId.PrintTimeIntervalControlInvalidDuration:
//                    return "间隔时间必须大于0且小于等于一天。";

//                case SchedulerStringId.PrintTimeIntervalControlInvalidStartEndTime:
//                    return "结束日期不能小于开始日期";

//                case SchedulerStringId.PrintTriFoldOptControlDailyCalendar:
//                    return "日历";

//                case SchedulerStringId.PrintTriFoldOptControlWeeklyCalendar:
//                    return "周历";

//                case SchedulerStringId.PrintTriFoldOptControlMonthlyCalendar:
//                    return "月历";

//                case SchedulerStringId.PrintWeeklyOptControlOneWeekPerPage:
//                    return "1 页/周";

//                case SchedulerStringId.PrintWeeklyOptControlTwoWeekPerPage:
//                    return "2 页/周";

//                case SchedulerStringId.PrintPageSetupFormCaption:
//                    return "打印选项: {0}";

//                case SchedulerStringId.PrintMoreItemsMsg:
//                    return "更多选项...";

//                case SchedulerStringId.PrintNoPrintersInstalled:
//                    return "没有安装打印机。";

//                case SchedulerStringId.Caption_FirstVisibleResources:
//                    return "首";

//                case SchedulerStringId.Caption_PrevVisibleResourcesPage:
//                    return "前一页";

//                case SchedulerStringId.Caption_PrevVisibleResources:
//                    return "上";

//                case SchedulerStringId.Caption_NextVisibleResources:
//                    return "下";

//                case SchedulerStringId.Caption_NextVisibleResourcesPage:
//                    return "下一页";

//                case SchedulerStringId.Caption_LastVisibleResources:
//                    return "最后";

//                case SchedulerStringId.Caption_IncreaseVisibleResourcesCount:
//                    return "增加可见资源数目";

//                case SchedulerStringId.Caption_DecreaseVisibleResourcesCount:
//                    return "减少可见资源数目";

//                case SchedulerStringId.Caption_ShadingApplyToAllDayArea:
//                    return "全天范围";

//                case SchedulerStringId.Caption_ShadingApplyToAppointments:
//                    return "日程安排";

//                case SchedulerStringId.Caption_ShadingApplyToAppointmentStatuses:
//                    return "日程安排状态";

//                case SchedulerStringId.Caption_ShadingApplyToHeaders:
//                    return "头部";

//                case SchedulerStringId.Caption_ShadingApplyToTimeRulers:
//                    return "时间标尺";

//                case SchedulerStringId.Caption_ShadingApplyToCells:
//                    return "单元";

//                case SchedulerStringId.Msg_InvalidSize:
//                    return "指定了无效的尺寸";

//                case SchedulerStringId.Msg_ApplyToAllStyles:
//                    return "所有格式都应用当前打印设置？";

//                case SchedulerStringId.Msg_MemoPrintNoSelectedItems:
//                    return "除选项之外都不能打印，请选择项再次打印";

//                case SchedulerStringId.Caption_AllResources:
//                    return "所有资源文件";

//                case SchedulerStringId.Caption_VisibleResources:
//                    return "显示资源文件";

//                case SchedulerStringId.Caption_OnScreenResources:
//                    return "在屏幕上的资源文件";

//                case SchedulerStringId.Caption_GroupByNone:
//                    return "无";

//                case SchedulerStringId.Caption_GroupByDate:
//                    return "日期";

//                case SchedulerStringId.Caption_GroupByResources:
//                    return "资源文件";

//                case SchedulerStringId.Msg_InvalidInputFile:
//                    return "输入的文件是无效";

//                case SchedulerStringId.TextRecurrenceTypeDaily:
//                    return "每天";

//                case SchedulerStringId.TextRecurrenceTypeWeekly:
//                    return "每周";

//                case SchedulerStringId.TextRecurrenceTypeMonthly:
//                    return "每月";

//                case SchedulerStringId.TextRecurrenceTypeYearly:
//                    return "每年";

//                case SchedulerStringId.TextRecurrenceTypeMinutely:
//                    return "每分";

//                case SchedulerStringId.TextRecurrenceTypeHourly:
//                    return "每时";

//                case SchedulerStringId.Msg_Warning:
//                    return "警告！";

//                case SchedulerStringId.Msg_CantFitIntoPage:
//                    return "无法采用当前打印设置将打印输出至单一页面。请尝试增加页面高度或减少PrintTime间隔。";

//                case SchedulerStringId.Msg_PrintStyleNameExists:
//                    return "'{0}'样式名已经存在，打印其他样式名";

//                case SchedulerStringId.Msg_OutlookCalendarNotFound:
//                    return "'{0}'日历没有找到";

//                case SchedulerStringId.Caption_PrevAppointment:
//                    return "上一个约会";

//                case SchedulerStringId.Caption_NextAppointment:
//                    return "下一个约会";

//                case SchedulerStringId.DisplayName_Appointment:
//                    return "约会";

//                case SchedulerStringId.Format_CopyOf:
//                    return "复制{0}";

//                case SchedulerStringId.Format_CopyNOf:
//                    return "复制{1}的({0})";

//                case SchedulerStringId.Msg_MissingRequiredMapping:
//                    return "属性'{0}'必须的映射丢失。";

//                case SchedulerStringId.Msg_MissingMappingMember:
//                    return "丢失属性'{0}'成员'{1}'的映射。";

//                case SchedulerStringId.Msg_DuplicateMappingMember:
//                    return "成员'{0}'的映射不唯一: ";

//                case SchedulerStringId.Msg_InconsistentRecurrenceInfoMapping:
//                    return "为了支持循环您必须映射循环信息和类型成员。";

//                case SchedulerStringId.Msg_IncorrectMappingsQuestion:
//                    return "不正确的映射。一定要继续吗？\r\n详细:\r\n";

//                case SchedulerStringId.Msg_DuplicateCustomFieldMappings:
//                    return "重复自定义字段名。修正映射: \r\n{0}";

//                case SchedulerStringId.Msg_MappingsCheckPassedOk:
//                    return "映射检查通过！";

//                case SchedulerStringId.Caption_SetupAppointmentMappings:
//                    return "设定约会映射";

//                case SchedulerStringId.Caption_SetupResourceMappings:
//                    return "设定资源映射";

//                case SchedulerStringId.Caption_SetupDependencyMappings:
//                    return "设定依赖映射";

//                case SchedulerStringId.Caption_ModifyAppointmentMappingsTransactionDescription:
//                    return "修改约会映射";

//                case SchedulerStringId.Caption_ModifyResourceMappingsTransactionDescription:
//                    return "修改资源映射";

//                case SchedulerStringId.Caption_ModifyAppointmentDependencyMappingsTransactionDescription:
//                    return "修改约会依赖映射";

//                case SchedulerStringId.Caption_MappingsValidation:
//                    return "映射确认";

//                case SchedulerStringId.Caption_MappingsWizard:
//                    return "映射向导...";

//                case SchedulerStringId.Caption_CheckMappings:
//                    return "检查映射";

//                case SchedulerStringId.Caption_SetupAppointmentStorage:
//                    return "设定约会存储";

//                case SchedulerStringId.Caption_SetupResourceStorage:
//                    return "设定资源存储";

//                case SchedulerStringId.Caption_SetupAppointmentDependencyStorage:
//                    return "设定依赖存储";

//                case SchedulerStringId.Caption_ModifyAppointmentStorageTransactionDescription:
//                    return "修改约会存储";

//                case SchedulerStringId.Caption_ModifyResourceStorageTransactionDescription:
//                    return "修改资源存储";

//                case SchedulerStringId.Caption_ModifyAppointmentDependencyStorageTransactionDescription:
//                    return "修改约会依赖存储";

//                case SchedulerStringId.Caption_DayViewDescription:
//                    return "切换到日视图。指定日的约会最详细的视图。";

//                case SchedulerStringId.Caption_WorkWeekViewDescription:
//                    return "切换到工作周视图。指定周内工作日的详细视图。";

//                case SchedulerStringId.Caption_WeekViewDescription:
//                    return "切换到周视图。在紧凑窗口内安排指定周的约会。";

//                case SchedulerStringId.Caption_MonthViewDescription:
//                    return "切换到月（多周）视图。便于长期计划的日历视图。";

//                case SchedulerStringId.Caption_TimelineViewDescription:
//                    return "切换到时间线视图。策划与时间相关的约会。";

//                case SchedulerStringId.Caption_GanttViewDescription:
//                    return "转化为甘特图视图. 根据时间部署约会.";

//                case SchedulerStringId.Caption_GroupByNoneDescription:
//                    return "按无分組";

//                case SchedulerStringId.Caption_GroupByDateDescription:
//                    return "按日期分組";

//                case SchedulerStringId.Caption_GroupByResourceDescription:
//                    return "按資源分組";

//                case SchedulerStringId.Msg_iCalendar_NotValidFile:
//                    return "无效的互联网日程文件";

//                case SchedulerStringId.Msg_iCalendar_AppointmentsImportWarning:
//                    return "不能导入一些约会";

//                case SchedulerStringId.MenuCmd_NavigateBackward:
//                    return "向后";

//                case SchedulerStringId.MenuCmd_NavigateForward:
//                    return "向前";

//                case SchedulerStringId.DescCmd_NavigateBackward:
//                    return "进入当前视图的上次建议";

//                case SchedulerStringId.DescCmd_NavigateForward:
//                    return "进入当前视图的后一次建议";

//                case SchedulerStringId.MenuCmd_ViewZoomIn:
//                    return "放大";

//                case SchedulerStringId.MenuCmd_ViewZoomOut:
//                    return "缩小";

//                case SchedulerStringId.DescCmd_ViewZoomIn:
//                    return "按比例放大显示更详细的内容";

//                case SchedulerStringId.DescCmd_ViewZoomOut:
//                    return "按比例缩小显示更详细的内容";

//                case SchedulerStringId.DescCmd_SplitAppointment:
//                    return "拆分约会";

//                case SchedulerStringId.Caption_SplitAppointment:
//                    return "拆分";

//                case SchedulerStringId.VS_SchedulerReportsToolboxCategoryName:
//                    return "DX.{0}:日程安排报表";

//                case SchedulerStringId.UD_SchedulerReportsToolboxCategoryName:
//                    return "日程安排组件";

//                case SchedulerStringId.Reporting_NotAssigned_TimeCells:
//                    return "需求提示单元组件没有指定";

//                case SchedulerStringId.Reporting_NotAssigned_View:
//                    return "需求视图组件没有指定";

//                case SchedulerStringId.Msg_RecurrenceExceptionsWillBeLost:
//                    return "与此相关的任何例外情况定期约会将会丢失。要继续吗？";

//                case SchedulerStringId.DescCmd_CreateAppointmentDependency:
//                    return "创建约会之间的依赖";

//                case SchedulerStringId.MenuCmd_CreateAppointmentDependency:
//                    return "创建依赖";

//                case SchedulerStringId.Caption_AppointmentDependencyTypeFinishToStart:
//                    return "完成到开始";

//                case SchedulerStringId.Caption_AppointmentDependencyTypeStartToStart:
//                    return "开始到开始";

//                case SchedulerStringId.Caption_AppointmentDependencyTypeFinishToFinish:
//                    return "完成到完成";

//                case SchedulerStringId.Caption_AppointmentDependencyTypeStartToFinish:
//                    return "开始到完成";
//            }
//            return base.GetLocalizedString(id);
//        }

//        public override string Language
//        {
//            get
//            {
//                return "简体中文";
//            }
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using XCI.Core;

namespace XCI.Helper
{
    /// <summary>
    /// 验证实用方法
    /// </summary>
    public static class ValidateHelper
    {

        /// <summary>
        /// 存储校验错误消息
        /// </summary>
        static ValidationMessagesConstant _messages = new ValidationMessagesConstant();

        /// <summary>
        /// 加载自定义错误消息，以覆盖默认值
        /// </summary>
        /// <param name="messages">自定义错误提示</param>
        public static void SetCustomMessages(ValidationMessagesConstant messages)
        {
            _messages = messages;
        }

        #region IValidatorBasic Members

        /// <summary>
        /// 验证字符串长度
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许空</param>
        /// <param name="checkMinLength">是否检测最小长度</param>
        /// <param name="checkMaxLength">是否检测最大长度</param>
        /// <param name="minLength">指定最小长度</param>
        /// <param name="maxLength">指定最大长度</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsStringLengthMatch(string text, bool allowNull, bool checkMaxLength, bool checkMinLength, int minLength, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
                return allowNull;

            if (checkMinLength && text.Length < minLength)
                return false;
            if (checkMaxLength && text.Length > maxLength)
                return false;

            return true;
        }

        /// <summary>
        /// 验证字符串长度
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许空</param>
        /// <param name="checkMinLength">是否检测最小长度</param>
        /// <param name="checkMaxLength">是否检测最大长度</param>
        /// <param name="minLength">指定最小长度</param>
        /// <param name="maxLength">指定最大长度</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsStringLengthMatch(string text, bool allowNull, bool checkMinLength, bool checkMaxLength, int minLength, int maxLength, Messages errors, string tag)
        {
            if (string.IsNullOrEmpty(text) && allowNull) return true;

            int textLength = text == null ? 0 : text.Length;

            if (checkMinLength && minLength > 0 && textLength < minLength)
                return CheckError(false, errors, tag, string.Format(_messages.TextLessThanMinLength, minLength));

            if (checkMaxLength && maxLength > 0 && textLength > maxLength)
                return CheckError(false, errors, tag, string.Format(_messages.TextMoreThanMaxLength, maxLength));

            return true;
        }


        /// <summary>
        /// 检测整数是否在指定的数字之间
        /// </summary>
        /// <param name="val">检测的数字</param>
        /// <param name="checkMin">是否检测最小值</param>
        /// <param name="checkMax">是否检测最大值</param>
        /// <param name="min">指定最小值</param>
        /// <param name="max">指定最大值</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsBetween(int val, bool checkMin, bool checkMax, int min, int max)
        {
            if (checkMin && val < min)
                return false;
            if (checkMax && val > max)
                return false;

            return true;
        }


        /// <summary>
        /// 检测字节数是否在指定的KB单位之间
        /// </summary>
        /// <param name="val">检测的字节数</param>
        /// <param name="checkMinLength">是否检测最小值</param>
        /// <param name="checkMaxLength">是否检测最大值</param>
        /// <param name="minKilobytes">指定最小KB值</param>
        /// <param name="maxKilobytes">指定最大KB值</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsSizeBetween(int val, bool checkMinLength, bool checkMaxLength, int minKilobytes, int maxKilobytes)
        {
            // 转换为千字节KB
            val = val / 1000;

            if (checkMinLength && val < minKilobytes)
                return false;
            if (checkMaxLength && val > maxKilobytes)
                return false;

            return true;
        }


        /// <summary>
        /// 字符串匹配正则表达式
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="regEx">正则表达式</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsStringRegExMatch(string text, bool allowNull, string regEx)
        {
            if (allowNull && string.IsNullOrEmpty(text))
                return true;

            return Regex.IsMatch(text, regEx);
        }

        /// <summary>
        /// 字符串匹配正则表达式
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="regEx">正则表达式</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsStringRegExMatch(string text, bool allowNull, string regEx, Messages errors, string tag)
        {
            return CheckErrorRegEx(text, allowNull, regEx, errors, tag, _messages.TextNotMatchPattern);
        }


        /// <summary>
        /// 检测文本是否包含在指定的值的字符串
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="compareCase">是否忽略大小写</param>
        /// <param name="allowedValues">允许的值 逗号分隔</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsStringIn(string text, bool allowNull, bool compareCase, string[] allowedValues, Messages errors, string tag)
        {
            bool isEmpty = string.IsNullOrEmpty(text);
            if (isEmpty && allowNull) return true;
            if (isEmpty)
            {
                string vals = StringHelper.ArrayToString(allowedValues);
                errors.Add(string.Format(_messages.TextMustBeIn, vals));
                return false;
            }
            bool isValid = false;
            foreach (string val in allowedValues)
            {
                if (string.Compare(text, val, compareCase) == 0)
                {
                    isValid = true;
                    break;
                }
            }
            if (!isValid)
            {
                string vals = StringHelper.ArrayToString(allowedValues);
                errors.Add(string.Format(_messages.TextMustBeIn, vals));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="text">输入字符串</param>
        /// <returns>成功返回true</returns>
        public static bool IsInteger(string text)
        {
            return Regex.IsMatch(text, RegexPatterns.Integer);
        }


        /// <summary>
        /// 指定文本是否是数字
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsNumeric(string text)
        {
            return Regex.IsMatch(text, RegexPatterns.Numeric);
        }

        /// <summary>
        /// 指定文本是否是数字
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsNumeric(string text, bool allowNull, Messages errors, string tag)
        {
            return CheckErrorRegEx(text, allowNull, RegexPatterns.Numeric, errors, tag, _messages.TextNotValidNumber);
        }

        /// <summary>
        /// 检测指定文本如果是文本 检测浮点数是否在指定的浮点数之间
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="checkMin">是否检测最小值</param>
        /// <param name="checkMax">是否检测最大值</param>
        /// <param name="min">指定最小值</param>
        /// <param name="max">指定最大值</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsNumericWithinRange(string text, bool checkMin, bool checkMax, double min, double max)
        {
            bool isNumeric = Regex.IsMatch(text, RegexPatterns.Numeric);
            if (!isNumeric) return false;

            double num = Double.Parse(text);
            return IsNumericWithinRange(num, checkMin, checkMax, min, max);
        }


        /// <summary>
        /// 检测浮点数是否在指定的浮点数之间
        /// </summary>
        /// <param name="num">检测的数字</param>
        /// <param name="checkMin">是否检测最小值</param>
        /// <param name="checkMax">是否检测最大值</param>
        /// <param name="min">指定最小值</param>
        /// <param name="max">指定最大值</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsNumericWithinRange(double num, bool checkMin, bool checkMax, double min, double max)
        {
            if (checkMin && num < min)
                return false;

            if (checkMax && num > max)
                return false;

            return true;
        }

        /// <summary>
        /// 检测指定文本如果是文本 检测浮点数是否在指定的浮点数之间
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="checkMin">是否检测最小值</param>
        /// <param name="checkMax">是否检测最大值</param>
        /// <param name="min">指定最小值</param>
        /// <param name="max">指定最大值</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsNumericWithinRange(string text, bool checkMin, bool checkMax, double min, double max, Messages errors, string tag)
        {
            bool isNumeric = Regex.IsMatch(text, RegexPatterns.Numeric);
            if (!isNumeric)
            {
                errors.Add(_messages.TextNotNumeric);
                return false;
            }

            double num = Double.Parse(text);
            return IsNumericWithinRange(num, checkMin, checkMax, min, max, errors, tag);
        }

        /// <summary>
        /// 检测浮点数是否在指定的浮点数之间
        /// </summary>
        /// <param name="num">检测的数字</param>
        /// <param name="checkMin">是否检测最小值</param>
        /// <param name="checkMax">是否检测最大值</param>
        /// <param name="min">指定最小值</param>
        /// <param name="max">指定最大值</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsNumericWithinRange(double num, bool checkMin, bool checkMax, double min, double max, Messages errors, string tag)
        {
            if (checkMin && num < min)
            {
                errors.Add(string.Format(_messages.NumberLessThan, min));
                return false;
            }

            if (checkMax && num > max)
            {
                errors.Add(string.Format(_messages.NumberMoreThan, max));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断文本是否小写/大写字母
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsAlpha(string text, bool allowNull)
        {
            return IsMatchRegEx(text, allowNull, RegexPatterns.Alpha);
        }

        /// <summary>
        /// 判断文本是否小写/大写字母
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsAlpha(string text, bool allowNull, Messages errors, string tag)
        {
            return CheckErrorRegEx(text, allowNull, RegexPatterns.Alpha, errors, tag, string.Format(_messages.TextMustContainOnlyChars, RegexPatterns.Alpha));
        }

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="text">输入字符串</param>
        /// <returns>成功返回true</returns>
        public static bool IsChinaChar(string text)
        {
            return IsMatchRegEx(text, false,RegexPatterns.ChinaChar);
        }

        /// <summary>
        /// 判断文本是否小写/大写字母或者数字
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsAlphaNumeric(string text, bool allowNull)
        {
            return IsMatchRegEx(text, allowNull, RegexPatterns.AlphaNumeric);
        }

        /// <summary>
        /// 判断文本是否小写/大写字母或者数字
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsAlphaNumeric(string text, bool allowNull, Messages errors, string tag)
        {
            return CheckErrorRegEx(text, allowNull, RegexPatterns.AlphaNumeric, errors, tag, string.Format(_messages.TextMustContainOnlyCharsAndNumbers, RegexPatterns.AlphaNumeric));
        }

        /// <summary>
        /// 判断文本是否是日期
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsDate(string text)
        {
            DateTime result = DateTime.MinValue;
            return DateTime.TryParse(text, out result);
        }

        /// <summary>
        /// 判断文本是否是日期
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsDate(string text, Messages errors, string tag)
        {
            DateTime result = DateTime.MinValue;
            return CheckError(DateTime.TryParse(text, out result), errors, tag, _messages.TextInvalidDate);
        }


        /// <summary>
        /// 检测日期是否是日期 检测是否在指定的日期之间
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="checkMin">是否检测最小日期</param>
        /// <param name="checkMax">是否检测最大日期</param>
        /// <param name="minDate">指定最小日期</param>
        /// <param name="maxDate">指定最大日期</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsDateWithinRange(string text, bool checkMin, bool checkMax, DateTime minDate, DateTime maxDate)
        {
            DateTime result = DateTime.MinValue;
            if (!DateTime.TryParse(text, out result)) return false;

            return IsDateWithinRange(result, checkMin, checkMax, minDate, maxDate);
        }


        /// <summary>
        /// 检测日期是否在指定的日期之间
        /// </summary>
        /// <param name="date">检测的日期</param>
        /// <param name="checkMin">是否检测最小日期</param>
        /// <param name="checkMax">是否检测最大日期</param>
        /// <param name="minDate">指定最小日期</param>
        /// <param name="maxDate">指定最大日期</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsDateWithinRange(DateTime date, bool checkMin, bool checkMax, DateTime minDate, DateTime maxDate)
        {
            if (checkMin && date.Date < minDate.Date) return false;
            if (checkMax && date.Date > maxDate.Date) return false;

            return true;
        }

        /// <summary>
        /// 检测日期是否是日期 检测是否在指定的日期之间
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="checkMin">是否检测最小日期</param>
        /// <param name="checkMax">是否检测最大日期</param>
        /// <param name="minDate">指定最小日期</param>
        /// <param name="maxDate">指定最大日期</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsDateWithinRange(string text, bool checkMin, bool checkMax, DateTime minDate, DateTime maxDate, Messages errors, string tag)
        {
            DateTime result = DateTime.MinValue;
            if (!DateTime.TryParse(text, out result))
            {
                errors.Add(_messages.TextInvalidDate);
                return false;
            }

            return IsDateWithinRange(result, checkMin, checkMax, minDate, maxDate, errors, tag);
        }


        /// <summary>
        /// 检测日期是否在指定的日期之间
        /// </summary>
        /// <param name="date">检测的日期</param>
        /// <param name="checkMin">是否检测最小日期</param>
        /// <param name="checkMax">是否检测最大日期</param>
        /// <param name="minDate">指定最小日期</param>
        /// <param name="maxDate">指定最大日期</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsDateWithinRange(DateTime date, bool checkMin, bool checkMax, DateTime minDate, DateTime maxDate, Messages errors, string tag)
        {
            if (checkMin && date.Date < minDate.Date)
            {
                errors.Add(string.Format(_messages.DateLessThanMinDate, minDate.ToShortDateString()));
                return false;
            }
            if (checkMax && date.Date > maxDate.Date)
            {
                errors.Add(string.Format(_messages.DateMoreThanMaxDate, maxDate.ToShortDateString()));
                return false;
            }

            return true;
        }


        /// <summary>
        /// 检测文本是否是一个时间格式 是否在指定的时间之间
        /// </summary>
        /// <param name="time">检测的文本</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsTimeSpan(string time)
        {
            TimeSpan span = TimeSpan.MinValue;
            return TimeSpan.TryParse(time, out span);
        }

        /// <summary>
        /// 检测文本是否是一个时间格式 是否在指定的时间之间
        /// </summary>
        /// <param name="time">检测的文本</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsTimeSpan(string time, Messages errors, string tag)
        {
            TimeSpan span = TimeSpan.MinValue;
            bool isMatch = TimeSpan.TryParse(time, out span);
            return CheckError(isMatch, errors, tag, _messages.TextInvalidTime);
        }

        /// <summary>
        /// 检测文本是否是一个时间格式 检测
        /// </summary>
        /// <param name="time">检测的文本</param>
        /// <param name="checkMin">是否检测最小日期</param>
        /// <param name="checkMax">是否检测最大日期</param>
        /// <param name="min">指定最小时间</param>
        /// <param name="max">指定最大时间</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsTimeSpanWithinRange(string time, bool checkMin, bool checkMax, TimeSpan min, TimeSpan max)
        {
            TimeSpan span = TimeSpan.MinValue;
            if (!TimeSpan.TryParse(time, out span))
                return false;

            return IsTimeSpanWithinRange(span, checkMin, checkMax, min, max);
        }


        /// <summary>
        /// 检测是否在指定的时间之间
        /// </summary>
        /// <param name="time">检测的文本</param>
        /// <param name="checkMin">是否检测最小日期</param>
        /// <param name="checkMax">是否检测最大日期</param>
        /// <param name="min">指定最小时间</param>
        /// <param name="max">指定最大时间</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsTimeSpanWithinRange(TimeSpan time, bool checkMin, bool checkMax, TimeSpan min, TimeSpan max)
        {
            if (checkMin && time.Ticks < min.Ticks) return false;
            if (checkMax && time.Ticks > max.Ticks) return false;

            return true;
        }

        /// <summary>
        /// 检测文本是否是一个时间格式 检测
        /// </summary>
        /// <param name="time">检测的文本</param>
        /// <param name="checkMin">是否检测最小日期</param>
        /// <param name="checkMax">是否检测最大日期</param>
        /// <param name="min">指定最小时间</param>
        /// <param name="max">指定最大时间</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsTimeSpanWithinRange(string time, bool checkMin, bool checkMax, TimeSpan min, TimeSpan max, Messages errors, string tag)
        {
            TimeSpan span = TimeSpan.MinValue;
            if (!TimeSpan.TryParse(time, out span))
                return CheckError(false, errors, tag, _messages.TextInvalidTime);

            return IsTimeSpanWithinRange(span, checkMin, checkMax, min, max, errors, tag);
        }


        /// <summary>
        /// 检测是否在指定的时间之间
        /// </summary>
        /// <param name="time">检测的文本</param>
        /// <param name="checkMin">是否检测最小日期</param>
        /// <param name="checkMax">是否检测最大日期</param>
        /// <param name="min">指定最小时间</param>
        /// <param name="max">指定最大时间</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsTimeSpanWithinRange(TimeSpan time, bool checkMin, bool checkMax, TimeSpan min, TimeSpan max, Messages errors, string tag)
        {
            if (checkMin && time.Ticks < min.Ticks) return false;
            if (checkMax && time.Ticks > max.Ticks) return false;
            return true;
        }


        /// <summary>
        /// 是否是手机号码
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsMobilePhone(string text, bool allowNull)
        {
            return IsMatchRegEx(text, allowNull, RegexPatterns.MobilePhone);
        }


        /// <summary>
        /// 是否是手机号码
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsMobilePhone(string text, bool allowNull, Messages errors, string tag)
        {
            return CheckErrorRegEx(text, allowNull, RegexPatterns.MobilePhone, errors, tag, _messages.TextInvalidUSPhone);
        }

        /// <summary>
        /// 是否是身份证号码
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsSsn(string text, bool allowNull)
        {
            return IsMatchRegEx(text, allowNull, RegexPatterns.SocialSecurity);
        }


        /// <summary>
        /// 是否是身份证号码
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsSsn(string text, bool allowNull, Messages errors, string tag)
        {
            return CheckErrorRegEx(text, allowNull, RegexPatterns.SocialSecurity, errors, tag, _messages.TextInvalidSSN);
        }



        /// <summary>
        /// 是否是Email
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsEmail(string text, bool allowNull)
        {
            return IsMatchRegEx(text, allowNull, RegexPatterns.Email);
        }

        /// <summary>
        /// 是否是Email
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsEmail(string text, bool allowNull, Messages errors, string tag)
        {
            return CheckErrorRegEx(text, allowNull, RegexPatterns.Email, errors, tag, _messages.TextInvalidEmail);
        }

        /// <summary>
        /// 是否是Url
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsUrl(string text, bool allowNull)
        {
            return IsMatchRegEx(text, allowNull, RegexPatterns.Url);
        }

        /// <summary>
        /// 是否是Url
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsUrl(string text, bool allowNull, Messages errors, string tag)
        {
            return CheckErrorRegEx(text, allowNull, RegexPatterns.Url, errors, tag, _messages.TextInvalidUrl);
        }


        /// <summary>
        /// 是否是邮政编码
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsZipCode(string text, bool allowNull)
        {
            return IsMatchRegEx(text, allowNull, RegexPatterns.ZipCode);
        }

        /// <summary>
        /// 是否是邮政编码
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsZipCode(string text, bool allowNull, Messages errors, string tag)
        {
            return CheckErrorRegEx(text, allowNull, RegexPatterns.ZipCode, errors, tag, _messages.TextInvalidUSZip);
        }

        /// <summary>
        /// 是否是IP
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsIP(string text, bool allowNull)
        {
            return IsMatchRegEx(text, allowNull, RegexPatterns.IP);
        }


        /// <summary>
        /// 是否是QQ
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsQQ(string text, bool allowNull)
        {
            return IsMatchRegEx(text, allowNull, RegexPatterns.QQ);
        }

        #endregion


        /// <summary>
        /// 检查文本是否匹配一个正则表达式
        /// </summary>
        /// <param name="text">检测的文本</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="regExPattern">正则表达式</param>
        /// <returns>验证成功返回True</returns>
        public static bool IsMatchRegEx(string text, bool allowNull, string regExPattern)
        {
            bool isEmpty = string.IsNullOrEmpty(text);
            if (isEmpty && allowNull) return true;
            if (isEmpty) return false;

            return Regex.IsMatch(text, regExPattern);
        }


        /// <summary>
        /// 检查条件 并添加到错误集合
        /// </summary>
        /// <param name="isValid">是否验证成功 失败添加到错误集合</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <param name="error">错误消息</param>
        /// <returns>是否验证成功</returns>
        public static bool CheckError(bool isValid, Messages errors, string tag, string error)
        {
            if (!isValid)
            {
                errors.Add(error);
            }
            return isValid;
        }


        /// <summary>
        /// 检查正则表达式 把错误消息添加到错误集合中
        /// </summary>
        /// <param name="inputText">检查的字符串</param>
        /// <param name="allowNull">是否允许为空</param>
        /// <param name="regExPattern">正则表达式</param>
        /// <param name="errors">错误集合</param>
        /// <param name="tag">错误消息Key</param>
        /// <param name="error">错误消息</param>
        /// <returns>是否验证成功</returns>
        public static bool CheckErrorRegEx(string inputText, bool allowNull, string regExPattern, Messages errors, string tag, string error)
        {
            bool isEmpty = string.IsNullOrEmpty(inputText);
            if (allowNull && isEmpty)
                return true;

            if (!allowNull && isEmpty)
            {
                errors.Add(error);
                return false;
            }

            bool isValid = Regex.IsMatch(inputText, regExPattern);
            if (!isValid) errors.Add(error);

            return isValid;
        }

    }

    /// <summary>
    /// 包含常用正则表达式常量
    /// </summary>
    public class RegexPatterns
    {
        /// <summary>
        /// 大小写字母正则
        /// </summary>
        public const string Alpha = @"^[a-zA-Z]*$";


        /// <summary>
        /// 中文字符
        /// </summary>
        public const string ChinaChar = @"[\u4e00-\u9fa5]";

        /// <summary>
        /// 大小字母正则
        /// </summary>
        public const string AlphaUpperCase = @"^[A-Z]*$";


        /// <summary>
        /// 小写字母正则
        /// </summary>
        public const string AlphaLowerCase = @"^[a-z]*$";


        /// <summary>
        /// 大小写字母数字正则
        /// </summary>
        public const string AlphaNumeric = @"^[a-zA-Z0-9]*$";


        /// <summary>
        /// 大小写字母数字空格正则
        /// </summary>
        public const string AlphaNumericSpace = @"^[a-zA-Z0-9 ]*$";


        /// <summary>
        /// 大小写字母数字空格和破折号正则
        /// </summary>
        public const string AlphaNumericSpaceDash = @"^[a-zA-Z0-9 \-]*$";


        /// <summary>
        /// 大小写字母数字空格和破折号和下划线正则
        /// </summary>
        public const string AlphaNumericSpaceDashUnderscore = @"^[a-zA-Z0-9 \-_]*$";


        /// <summary>
        /// 大小写字母数字空格和破折号和点和下划线正则
        /// </summary>
        public const string AlphaNumericSpaceDashUnderscorePeriod = @"^[a-zA-Z0-9\. \-_]*$";


        /// <summary>
        /// 数字正则
        /// </summary>
        public const string Numeric = @"^\-?[0-9]*\.?[0-9]*$";


        /// <summary>
        /// 整数正则
        /// </summary>
        public const string Integer = @"^\-?[0-9]*$";


        /// <summary>
        /// 身份证正则
        /// </summary>
        public const string SocialSecurity = @"^\d{15}$|^\d{17}(?:\d|x|X)$";


        /// <summary>
        /// E-mail正则.
        /// </summary>
        public const string Email = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";


        /// <summary>
        /// Url正则
        /// </summary>
        public const string Url = @"^^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_=]*)?$";


        /// <summary>
        /// 邮政编码正则
        /// </summary>
        public const string ZipCode = @"[1-9]\d{5}(?!\d)";


        /// <summary>
        /// 移动电话正则
        /// </summary>
        public const string MobilePhone = @"^(13[0-9]|15[0|3|6|7|8|9]|18[0|8|9])\d{8}$";


        /// <summary>
        /// IP地址正则
        /// </summary>
        public const string IP = @"\d+\.\d+\.\d+\.\d+";


        /// <summary>
        /// QQ号码
        /// </summary>
        public const string QQ = @"[1-9][0-9]{4,}";
    }


    /// <summary>
    /// 用来存储校验错误消息
    /// </summary>
    public class ValidationMessagesConstant
    {
        /// <summary>
        /// 文本长度小于最小长度
        /// </summary>
        public string TextLessThanMinLength { get; set; }


        /// <summary>
        /// 文本长度大于最大长度
        /// </summary>
        public string TextMoreThanMaxLength { get; set; }


        /// <summary>
        /// 不匹配表达式
        /// </summary>
        public string TextNotMatchPattern { get; set; }


        /// <summary>
        /// 文字必须在内
        /// </summary>
        public string TextMustBeIn { get; set; }


        /// <summary>
        /// 不是有效的数字
        /// </summary>
        public string TextNotValidNumber { get; set; }


        /// <summary>
        /// 不是有效的数字
        /// </summary>
        public string TextNotNumeric { get; set; }


        /// <summary>
        /// 数字小于
        /// </summary>
        public string NumberLessThan { get; set; }


        /// <summary>
        /// 数字大于
        /// </summary>
        public string NumberMoreThan { get; set; }


        /// <summary>
        /// 文本提供必须只包含字符
        /// </summary>
        public string TextMustContainOnlyChars { get; set; }


        /// <summary>
        /// 文本提供必须只包含字符和数字
        /// </summary>
        public string TextMustContainOnlyCharsAndNumbers { get; set; }


        /// <summary>
        /// 不是有效的日期
        /// </summary>
        public string TextInvalidDate { get; set; }


        /// <summary>
        /// 日期小于最小日期
        /// </summary>
        public string DateLessThanMinDate { get; set; }


        /// <summary>
        /// 日期大于最大日期
        /// </summary>
        public string DateMoreThanMaxDate { get; set; }


        /// <summary>
        /// 不是有效的时间
        /// </summary>
        public string TextInvalidTime { get; set; }


        /// <summary>
        /// 不是有效的移动电话号码
        /// </summary>
        public string TextInvalidUSPhone { get; set; }


        /// <summary>
        /// 不是有效的身份证号码
        /// </summary>
        public string TextInvalidSSN { get; set; }


        /// <summary>
        /// 不是有效的Email地址
        /// </summary>
        public string TextInvalidEmail { get; set; }


        /// <summary>
        /// 不是有效的Url地址
        /// </summary>
        public string TextInvalidUrl { get; set; }


        /// <summary>
        /// 不是有效的邮政编码
        /// </summary>
        public string TextInvalidUSZip { get; set; }


        /// <summary>
        /// 对象不同
        /// </summary>
        public string ObjectsAreNotEqual { get; set; }


        /// <summary>
        /// 对象相同
        /// </summary>
        public string ObjectsAreEqual { get; set; }


        /// <summary>
        /// 不支持
        /// </summary>
        public string IsNotSupplied { get; set; }


        /// <summary>
        /// 默认构造
        /// </summary>
        /// <param name="TextLessThanMinLength">文本长度小于最小长度</param>
        /// <param name="TextMoreThanMaxLength">文本长度大于最大长度</param>
        /// <param name="TextNotMatchPattern">不匹配表达式</param>
        /// <param name="TextMustBeIn">文字必须在内</param>
        /// <param name="TextNotValidNumber">不是有效的数字</param>
        /// <param name="TextNotNumeric">不是有效的数字</param>
        /// <param name="NumberLessThan">数字小于</param>
        /// <param name="NumberMoreThan">数字大于</param>
        /// <param name="TextMustContainOnlyChars">文本提供必须只包含字符</param>
        /// <param name="TextMustContainOnlyCharsAndNumbers">文本提供必须只包含字符和数字</param>
        /// <param name="TextInvalidDate">不是有效的日期</param>
        /// <param name="DateLessThanMinDate">日期小于最小日期</param>
        /// <param name="DateMoreThanMaxDate">日期大于最大日期</param>
        /// <param name="TextInvalidTime">不是有效的时间</param>
        /// <param name="TextInvalidUSPhone">不是有效的移动电话号码</param>
        /// <param name="TextInvalidSSN">不是有效的身份证号码</param>
        /// <param name="TextInvalidEmail">不是有效的Email地址</param>
        /// <param name="TextInvalidUrl">不是有效的Url地址</param>
        /// <param name="TextInvalidUSZip">不是有效的邮政编码</param>
        /// <param name="ObjectsAreNotEqual">对象不同</param>
        /// <param name="ObjectsAreEqual">对象相同</param>
        /// <param name="IsNotSupplied">不支持</param>
        public ValidationMessagesConstant(string TextLessThanMinLength = DefaultValues.DEFAULT_TEXT_LESS_THAN_MIN_LENGTH,
                                           string TextMoreThanMaxLength = DefaultValues.DEFAULT_TEXT_MORE_THAN_MAX_LENGTH,
                                           string TextNotMatchPattern = DefaultValues.DEFAULT_TEXT_NOT_MATCH_PATTERN,
                                           string TextMustBeIn = DefaultValues.DEFAULT_TEXT_MUST_BE_IN,
                                           string TextNotValidNumber = DefaultValues.DEFAULT_TEXT_NOT_VALID_NUMBER,
                                           string TextNotNumeric = DefaultValues.DEFAULT_TEXT_NOT_NUMERIC,
                                           string NumberLessThan = DefaultValues.DEFAULT_NUMBER_LESS_THAN,
                                           string NumberMoreThan = DefaultValues.DEFAULT_NUMBER_MORE_THAN,
                                           string TextMustContainOnlyChars = DefaultValues.DEFAULT_TEXT_MUST_CONTAIN_ONLY_CHARS,
                                           string TextMustContainOnlyCharsAndNumbers = DefaultValues.DEFAULT_TEXT_MUST_CONTAIN_ONLY_CHARS_AND_NUMBERS,
                                           string TextInvalidDate = DefaultValues.DEFAULT_TEXT_INVALID_DATE,
                                           string DateLessThanMinDate = DefaultValues.DEFAULT_DATE_LESS_THAN_MIN_DATE,
                                           string DateMoreThanMaxDate = DefaultValues.DEFAULT_DATE_MORE_THAN_MAX_DATE,
                                           string TextInvalidTime = DefaultValues.DEFAULT_TEXT_INVALID_TIME,
                                           string TextInvalidUSPhone = DefaultValues.DEFAULT_TEXT_INVALID_US_PHONE,
                                           string TextInvalidSSN = DefaultValues.DEFAULT_TEXT_INVALID_SSN,
                                           string TextInvalidEmail = DefaultValues.DEFAULT_TEXT_INVALID_EMAIL,
                                           string TextInvalidUrl = DefaultValues.DEFAULT_TEXT_INVALID_URL,
                                           string TextInvalidUSZip = DefaultValues.DEFAULT_TEXT_INVALID_US_ZIP,
                                           string ObjectsAreNotEqual = DefaultValues.DEFAULT_OBJECTS_ARE_NOT_EQUAL,
                                           string ObjectsAreEqual = DefaultValues.DEFAULT_OBJECTS_ARE_EQUAL,
                                           string IsNotSupplied = DefaultValues.DEFAULT_IS_NOT_SUPPLIED)
        {
            Guard.IsTrue(TextLessThanMinLength.Contains("{0}"), "文本长度小于最小长度需要一个参数");
            Guard.IsTrue(TextMoreThanMaxLength.Contains("{0}"), "文本长度大于最大长度需要一个参数");
            Guard.IsTrue(TextMustBeIn.Contains("{0}"), "文字必须在内要一个参数");
            Guard.IsTrue(NumberLessThan.Contains("{0}"), "数字小于消息需要一个参数");
            Guard.IsTrue(NumberMoreThan.Contains("{0}"), "数字大于消息需要一个参数");
            Guard.IsTrue(TextMustContainOnlyChars.Contains("{0}"), "文本提供必须只包含字符需要一个参数");
            Guard.IsTrue(TextMustContainOnlyCharsAndNumbers.Contains("{0}"), "文本提供必须只包含字符和数字需要一个参数");
            Guard.IsTrue(DateLessThanMinDate.Contains("{0}"), "日期小于最小日期需要一个参数");
            Guard.IsTrue(DateMoreThanMaxDate.Contains("{0}"), "日期大于最大日期需要一个参数");

            this.TextLessThanMinLength = TextLessThanMinLength;
            this.TextMoreThanMaxLength = TextMoreThanMaxLength;
            this.TextNotMatchPattern = TextNotMatchPattern;
            this.TextMustBeIn = TextMustBeIn;
            this.TextNotValidNumber = TextNotValidNumber;
            this.TextNotNumeric = TextNotNumeric;
            this.NumberLessThan = NumberLessThan;
            this.NumberMoreThan = NumberMoreThan;
            this.TextMustContainOnlyChars = TextMustContainOnlyChars;
            this.TextMustContainOnlyCharsAndNumbers = TextMustContainOnlyCharsAndNumbers;
            this.TextInvalidDate = TextInvalidDate;
            this.DateLessThanMinDate = DateLessThanMinDate;
            this.DateMoreThanMaxDate = DateMoreThanMaxDate;
            this.TextInvalidTime = TextInvalidTime;
            this.TextInvalidUSPhone = TextInvalidUSPhone;
            this.TextInvalidSSN = TextInvalidSSN;
            this.TextInvalidEmail = TextInvalidEmail;
            this.TextInvalidUrl = TextInvalidUrl;
            this.TextInvalidUSZip = TextInvalidUSZip;
            this.ObjectsAreNotEqual = ObjectsAreNotEqual;
            this.ObjectsAreEqual = ObjectsAreEqual;
            this.IsNotSupplied = IsNotSupplied;
        }


        /// <summary>
        /// 保存默认的消息值
        /// </summary>
        internal class DefaultValues
        {
            public const string DEFAULT_TEXT_LESS_THAN_MIN_LENGTH = "文本长度小于最小长度 ({0})";
            public const string DEFAULT_TEXT_MORE_THAN_MAX_LENGTH = "文本长度大于最大长度 ({0})";
            public const string DEFAULT_TEXT_NOT_MATCH_PATTERN = "不匹配表达式";
            public const string DEFAULT_TEXT_MUST_BE_IN = "文字必须在 {0} 内";
            public const string DEFAULT_TEXT_NOT_VALID_NUMBER = "不是有效的数字";
            public const string DEFAULT_TEXT_NOT_NUMERIC = "不是有效的数字";
            public const string DEFAULT_NUMBER_LESS_THAN = "数字小于 {0}";
            public const string DEFAULT_NUMBER_MORE_THAN = "数字大于 {0}";
            public const string DEFAULT_TEXT_MUST_CONTAIN_ONLY_CHARS = "文本提供必须只包含字符 {0}";
            public const string DEFAULT_TEXT_MUST_CONTAIN_ONLY_CHARS_AND_NUMBERS = "文本提供必须只包含字符和数字 {0}";
            public const string DEFAULT_TEXT_INVALID_DATE = "不是有效的日期";
            public const string DEFAULT_DATE_LESS_THAN_MIN_DATE = "日期小于最小日期 {0}";
            public const string DEFAULT_DATE_MORE_THAN_MAX_DATE = "日期大于最大日期 {0}";
            public const string DEFAULT_TEXT_INVALID_TIME = "不是有效的时间";
            public const string DEFAULT_TEXT_INVALID_US_PHONE = "不是有效的移动电话号码";
            public const string DEFAULT_TEXT_INVALID_SSN = "不是有效的身份证号码";
            public const string DEFAULT_TEXT_INVALID_EMAIL = "不是有效的Email地址";
            public const string DEFAULT_TEXT_INVALID_URL = "不是有效的Url地址";
            public const string DEFAULT_TEXT_INVALID_US_ZIP = "不是有效的邮政编码";
            public const string DEFAULT_OBJECTS_ARE_NOT_EQUAL = "对象不同";
            public const string DEFAULT_OBJECTS_ARE_EQUAL = "对象相同";
            public const string DEFAULT_IS_NOT_SUPPLIED = " 不支持";
        }
    }

}

//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MDL
{
    using System;
    
    public partial class P_QUERY_MY_EXAMS_Result
    {
        public Nullable<System.Guid> ID { get; set; }
        public string EXAM_PLAN_NAME { get; set; }
        public Nullable<System.Guid> EXAM_TYPE_ID { get; set; }
        public string EXAM_TYPE_NAME { get; set; }
        public Nullable<System.Guid> PAPER_ID { get; set; }
        public string PAPER_NAME { get; set; }
        public decimal TOTAL_SCORE { get; set; }
        public int TOTAL_TIME { get; set; }
        public string PAPER_MODEL { get; set; }
        public string EXAM_TIME_PERIOD { get; set; }
        public string PAPER_MODEL_NAME { get; set; }
    }
}
using Microsoft.Data.SqlClient;

namespace SchoolSchedule.Application.Mapping.DBUpdateExceptions
{
    public static class DBUpdateExceptionsMapper
    {
        public static string MapToDbUpdateExceptionMessage(this SqlException sqlEx)
        {
            string message = sqlEx.Number switch
            {
                2627 => "لا يمكن إضافة هذا السجل لأنه موجود بالفعل.",
                2601 => "هذه القيمة مستخدمة بالفعل ولا يمكن تكرارها.",
                547 => "لا يمكن تنفيذ العملية بسبب وجود علاقة مع بيانات أخرى.",
                515 => "هذا الحقل مطلوب ولا يمكن أن يكون فارغًا.",
                8152 => "النص المدخل أطول من المسموح به.",
                245 => "نوع البيانات غير صالح للحقل.",
                _ => "حدث خطأ غير متوقع أثناء التعامل مع قاعدة البيانات."
            };
            return message;
        }
    }
}

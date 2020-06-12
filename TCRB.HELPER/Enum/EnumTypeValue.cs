using System.ComponentModel;

namespace TCRB.HELPER.Enum
{
    public enum EnumTypeValue
    {
        [Description("String")]
        String = 1,
        //[Description("Int")]
        //Int = 2,
        //[Description("Int16")]
        //Int16 = 3,
        [Description("Int32")]
        Int32 = 4,
        [Description("decimal")]
        Decimal = 5,
        [Description("Boolean")]
        Boolean = 6,
        [Description("DateTime")]
        DateTime = 7,
    }
}

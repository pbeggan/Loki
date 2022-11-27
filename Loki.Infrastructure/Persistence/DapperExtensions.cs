using Dapper;

namespace Loki.Infrastructure.Persistence
{
    public static class DapperExtensions
    {
        /// <summary>
        ///     Creates a nvarchar database datatype with the specified length. Uses MAX if null specified
        /// </summary>
        public static DbString AsNVarChar(this string value, int? maxLength = null)
        {
            return new DbString { Value = value, IsFixedLength = false, IsAnsi = false, Length = maxLength ?? -1 };
        }

        /// <summary>
        ///     Creates a varchar database datatype with the specified length. Uses MAX if null specified
        /// </summary>
        public static DbString AsVarChar(this string value, int? maxLength = null)
        {
            return new DbString { Value = value, IsFixedLength = false, IsAnsi = true, Length = maxLength ?? -1 };
        }

        /// <summary>
        ///     Creates a char database datatype with the specified length.
        /// </summary>
        public static DbString AsChar(this string value, int length)
        {
            return new DbString { Value = value, IsFixedLength = true, IsAnsi = true, Length = length };
        }

        /// <summary>
        ///     Creates a nchar database datatype with the specified length.
        /// </summary>
        public static DbString AsNChar(this string value, int length)
        {
            return new DbString { Value = value, IsFixedLength = true, IsAnsi = false, Length = length };
        }
    }
}


using GT.TripManagementService.Domain.Utils;
using System.Reflection;

namespace GT.TripManagementService.Domain.Constant
{
    public static class EnumHelper
    {
        public static string Name<T>(this T srcValue) => GetCustomName(typeof(T).GetField(srcValue?.ToString() ?? string.Empty));
        private static string GetCustomName(FieldInfo? fi)
        {
            Type type = typeof(CustomName);

            Attribute? attr = null;
            if (fi is not null)
            {
                attr = Attribute.GetCustomAttribute(fi, type);
            }

            return (attr as CustomName)?.Name ?? string.Empty;
        }
        public enum TripStatus
        {
            Draf,
            Published
           
        }
        public enum ExperienceLevel
        {
            None,
            Basic,
            Intermediate,
            Advanced
        }
        public enum DepartureStatus
        {
            Ready,
            Full,
            InProgress,
            Completed,
            Canceled
        }
    }
}

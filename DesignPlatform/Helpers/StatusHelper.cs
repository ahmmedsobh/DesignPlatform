using DesignPlatform.Enums;

namespace DesignPlatform.Helpers
{
    public class StatusHelper
    {
        public static string StatusText(int status)
        {
            var StatusText = "";
            switch (status)
            {
                case (int)ProjectStatus.Pending:
                    StatusText = "Pending";
                    break;
                case (int)ProjectStatus.Assigned:
                    StatusText = "Assigned";
                    break;
                case (int)ProjectStatus.Uploaded:
                    StatusText = "Uploaded";
                    break;
                case (int)ProjectStatus.Declined:
                    StatusText = "Declined";
                    break;
                case (int)ProjectStatus.Approved:
                    StatusText = "Approved";
                    break;
                case (int)ProjectStatus.Revision:
                    StatusText = "Revision";
                    break;
            }

            return StatusText;
        }
    }
}

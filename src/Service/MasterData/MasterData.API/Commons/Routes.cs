namespace MasterData.API.Commons
{
    public class Routes
    {
        /// <summary>
        /// base route
        /// </summary>
        public static class UnitRoutes
        {
            public const string Prefix = @"master-data/api/unit";
            public const string List = "list";
        }

        /// <summary>
        /// thuộc tính
        /// </summary>
        public static class AttributeRoutes
        {
            public const string Prefix = @"master-data/api/attribute";
            public const string List = "list";
            public const string Toggle = "toggle";
        }

        /// <summary>
        /// người dùng
        /// </summary>
        public static class UserRoutes
        {
            public const string Prefix = @"master-data/api/user";
            public const string Create = "create";
            public const string Delete = "del";
            public const string List = "all";
            public const string Update = "update";
            public const string UserDetail = "detail";
            public const string LockUser = "lock";
            public const string UnLockUser = "Unlock";
            public const string ChangeAvatar = "avatar-change";
            public const string ChangePassword = "password-change";
        }

        /// <summary>
        /// xác thực người dùng
        /// </summary>
        public static class UserAuthenRoutes
        {
            public const string Prefix = @"master-data/api/user/authen";
            public const string AddInfo = "add-authen-info";
            public const string SendInfo = "send-authen-info";
            public const string confirm = "authen-confirm-info";
        }

        /// <summary>
        /// Khiếu nại
        /// </summary>
        public static class ComplainRoutes
        {
            public const string Prefix = @"master-data/api/complain";
            public const string Create = "create";
            public const string Send = "send";
            public const string Detail = "detail";
            public const string Feedback = "feedback";
            public const string Delete = "del";
            public const string List = "list";
        }

        /// <summary>
        /// Phản hồi khiếu nại
        /// </summary>
        public static class ComplainReplyRoutes
        {
            public const string Prefix = @"master-data/api/complain/reply";
            public const string Create = "create";
            public const string Delete = "del";
            public const string UpdateContent = "change-content";
        }

        /// <summary>
        /// Thông báo
        /// </summary>
        public static class NotificationRoutes
        {
            public const string Prefix = @"master-data/api/notification";
            public const string Create = "create";
            public const string Detail = "detail";
            public const string ListReceived = "list-received";
            public const string ListSended = "list-sended";
            public const string SendById = "send";
            public const string SendAll = "sendall";
        }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public static class StatusRoutes
        {
            public const string Prefix = @"master-data/api/status";
            public const string List = "list";
            public const string Create = "create";
            public const string Delete = "del";
            public const string Update = "update";
        }

        /// <summary>
        /// Vị trí
        /// </summary>
        public static class LocationRoutes
        {
            public const string Prefix = @"master-data/api/map-location";
            public const string List = "list";
            public const string Create = "create";
            public const string Delete = "del";
            public const string Update = "update";
        }

        /// <summary>
        /// Loại vé
        /// </summary>
        public static class CategoryTicketRoutes
        {
            public const string Prefix = @"master-data/api/category-ticket";
            public const string List = "list";
            public const string Create = "create";
            public const string Delete = "del";
            public const string Update = "update";
        }

        /// <summary>
        /// Giao dịch
        /// </summary>
        public static class TransactionRoutes
        {
            public const string Prefix = @"master-data/api/transaction";
            public const string Recharge = "recharge";
            public const string ListAll = "all-list";
            public const string ListUser = "user-list";
            public const string Detail = "detail";
        }
    }
}

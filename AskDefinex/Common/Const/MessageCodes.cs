namespace AskDefinex.Common.Const
{
    public static class MessageCodes
    {
        //Common 20000

        //User 21000

        //Auth
        public const string LOGIN_FAILED = "10001";

        //User
        public const string USER_NO_DATA_FOUND = "10101";//Bu maile ait kullanıcı tanımı bulunamamıştır.
        public const string USER_ALREADY_EXIST = "10102";//Girdiğiniz email ile kayıtlı kullanıcı sistemde bulunmaktadır.
        //
        public const string UserCreationFailedError = "11001";
        public const string UserDetailFailedError = "11002";
        public const string UserUpdateFailedError = "11003";
        public const string UserDeletionFailedError = "11004";
        public const string UserListFailedError = "11005";

        //Question
        public const string QUESTION_NO_DATA_FOUND = "10200";
        public const string QUESTION_ALREADY_EXIST = "10201";

        //Recaptcha
        public const string RECAPTCHA_FAILED = "3131"; // Recaptcha score low
    }
}

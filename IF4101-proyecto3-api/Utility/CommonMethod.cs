using IF4101_proyecto3_web.Data;

namespace IF4101_proyecto3_api.Utility
{
    public static class CommonMethod
    {
        public static bool ReadParameterReturn(ConnectionDb connectionDb)
        {
            if ((int)connectionDb.ParameterReturn.Value == 1)
                return true;

            connectionDb.SqlConnection.Close();
            return false;
        }
    }
}

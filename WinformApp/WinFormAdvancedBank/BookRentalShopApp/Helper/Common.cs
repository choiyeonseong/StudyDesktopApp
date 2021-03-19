using System.Net;

namespace BookRentalShopApp.Helper
{
    public class Common
    {
        public static string ConnString = "Data Source=127.0.0.1;" +
                                          "Initial Catalog=bookrentalshop;" +
                                          "Persist Security Info=True;" +
                                          "User ID=sa;" +
                                          "Password=mssql_p@ssw0rd!";

        public static string LoginUserId = string.Empty;

        /// <summary>
        /// IP 주소 받아오는 메서드
        /// </summary>
        /// <returns></returns>
        internal static string GetLocalIp()
        {
            string localIP = "";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }

            return localIP;
        }
        /// <summary>
        /// Sql Injection 방지하기 위해
        /// 특수문자 치환
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        internal static string ReplaceCmdText(string strSource)
        {
            var result = strSource.Replace("'", "＇");   // 홑따옴표를 특수문자로 치환
            result = result.Replace("--", "");  // --(주석처리) 사용 금지
            result = result.Replace(";", "");   // ; 사용 금지

            return result;
        }
    }
}

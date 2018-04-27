using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.IO;
/// <summary>
///ValidateUser 的摘要说明
/// </summary>
public class ValidateUser
{
	public ValidateUser()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

        // logon types
        const int LOGON32_LOGON_INTERACTIVE = 2;
        const int LOGON32_LOGON_NETWORK = 3;
        const int LOGON32_LOGON_NEW_CREDENTIALS = 9;
        // logon providers
        const int LOGON32_PROVIDER_DEFAULT = 0;
        const int LOGON32_PROVIDER_WINNT50 = 3;
        const int LOGON32_PROVIDER_WINNT40 = 2;
        const int LOGON32_PROVIDER_WINNT35 = 1;

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int LogonUser(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        private WindowsImpersonationContext impersonationContext;

        public bool impersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                // 这里使用LOGON32_LOGON_NEW_CREDENTIALS来访问远程资源。
                // 如果要（通过模拟用户获得权限）实现服务器程序，访问本地授权数据库可
                // 以用LOGON32_LOGON_INTERACTIVE
              if (LogonUser(userName, domain, password, LOGON32_LOGON_NEW_CREDENTIALS,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                 //LogonUser("myname", "192.168.1.48", "password", LOGON32_LOGON_NEW_CREDENTIALS,
              //  LOGON32_PROVIDER_DEFAULT, ref token);
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            System.AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                            IPrincipal pr = System.Threading.Thread.CurrentPrincipal;
                            IIdentity id = pr.Identity;
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }

            if (token != IntPtr.Zero)
                CloseHandle(token);

            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);

            return false;
        }

        public void undoImpersonation()
        {
            impersonationContext.Undo();
        }

        //public void TestFunc()
        //{
        //    bool isImpersonated = false;
        //    try
        //    {
        //        //LogonUser("myname", "192.168.1.48", "password", LOGON32_LOGON_NEW_CREDENTIALS,
        //      //  LOGON32_PROVIDER_DEFAULT, ref token);


        //        if (impersonateValidUser("asd", "192.168.100.1", "Pass123"))
        //        {
        //            isImpersonated = true;
        //            //do what you want now, as the special user
        //            // ...
        //            File.Copy(@"\\192.168.100.1\d$\test\11.txt", @"d:\\data\11.txt", true);
        //        }
        //    }
        //    finally
        //    {
        //        if (isImpersonated)
        //            undoImpersonation();
        //    }
        //}

    }



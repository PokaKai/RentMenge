using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using rent.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace rent
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var credentialUserName = "rentmcutest@gmail.com";
            var sentFrom = "rentmcutest@gmail.com";
            var pwd = "jfjlgvlglqmfhnzm";

            // Configure the client:
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com");

            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Creatte the credentials:
            System.Net.NetworkCredential credentials =
            new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            var mail =
            new System.Net.Mail.MailMessage(sentFrom, message.Destination);

            mail.Subject = message.Subject;
            mail.Body = message.Body;

            // Send:
            return client.SendMailAsync(mail);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var accountSid = ConfigurationManager.AppSettings["SMSAccountIdentification"];
            var authToken = ConfigurationManager.AppSettings["SMSAccountPassword"];
            var fromNumber = ConfigurationManager.AppSettings["SMSAccountFrom"];
            TwilioClient.Init(accountSid, authToken);
            MessageResource result = MessageResource.Create(
                new PhoneNumber(message.Destination),
                from: new PhoneNumber(fromNumber),
               body: message.Body
            );
            Trace.TraceInformation(result.Status.ToString());
            return Task.FromResult(0);
        }
    }

    // 設定此應用程式中使用的應用程式使用者管理員。UserManager 在 ASP.NET Identity 中定義且由應用程式中使用。
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // 設定使用者名稱的驗證邏輯
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // 設定密碼的驗證邏輯
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // 設定使用者鎖定詳細資料
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // 註冊雙因素驗證提供者。此應用程式使用手機和電子郵件接收驗證碼以驗證使用者
            // 您可以撰寫專屬提供者，並將它外掛到這裡。
            manager.RegisterTwoFactorProvider("電話代碼", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "您的安全碼為 {0}"
            });
            manager.RegisterTwoFactorProvider("電子郵件代碼", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "安全碼",
                BodyFormat = "您的安全碼為 {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // 設定在此應用程式中使用的應用程式登入管理員。
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}

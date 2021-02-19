using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class SendFeedback : MonoBehaviour
{
    [SerializeField] InputField textMess;
    [SerializeField] InputField email;
    [SerializeField] InputField passwordPlayer;

    [SerializeField] GameObject feedbackAlert;
    [SerializeField] GameObject ExeptionAlert;





    //public void SendMess()
    //{
    //    if(IsValidEmailFormat(email.text) == "Input correct e-mail")
    //    {
    //        ExeptionAlert.SetActive(true);
    //    }

    //    MailMessage message = new MailMessage();
    //    message.Subject = "Feedback about Boom Battle";
    //    message.Body = textMess.text;

    //    message.From = new MailAddress(IsValidEmailFormat(email.text));
    //    message.To.Add("shch.dima@yandex.ru");
    //    message.BodyEncoding = System.Text.Encoding.UTF8;

    //    SmtpClient client = new SmtpClient();
    //    client.Host = "smtp.gmail.com";
    //    client.Port = 587;
    //    client.Credentials = new NetworkCredential(message.From.Address, passwordPlayer.text);
    //    client.EnableSsl = true;

    //    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //    {
    //        return true;
    //    };
        

    //    client.Send(message);
    //    Debug.Log("success");
    //    BackHelp();
    //}

    //public static Regex _regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);

    //public static string IsValidEmailFormat(string emailInput)
    //{
    //    if(_regex.IsMatch(emailInput))
    //    {
    //        return emailInput;
    //    }
    //    else
    //    {
    //       return emailInput = "Input correct e-mail";
    //    }
    //}


    //public void SendMes()
    //{
    //    if (IsValidEmailFormat(email.text) == "Input correct e-mail")
    //    {
    //        ExeptionAlert.SetActive(true);
    //    }

    //    MailMessage mail = new MailMessage();
    //    string log = IsValidEmailFormat(email.text);
    //    string pas = passwordPlayer.text;
    //    Debug.Log(log);
    //    mail.From = new MailAddress(log);
    //    mail.To.Add("silent97vvaaavava@yandex.ru");
    //    mail.Subject = "Feedback about Boom Battle";
    //    mail.Body = "This is for testing SMTP mail from GMAIL";

    //    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
    //    smtpServer.Port = 587;
    //    smtpServer.Credentials = new System.Net.NetworkCredential(mail.From.Address, pas) as ICredentialsByHost;
    //    smtpServer.EnableSsl = true;
    //    ServicePointManager.ServerCertificateValidationCallback =
    //        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //        { return true; };
    //    smtpServer.Send(mail);
    //    Debug.Log("success");
    //}

    //public void BackHelp()
    //{
    //    if (feedbackAlert.activeSelf)
    //    {
    //        feedbackAlert.SetActive(false);
    //    }
    //    else
    //    {
    //        feedbackAlert.SetActive(true);
    //    } 
    //}

    string subject = "Report bug";
    string body = "Description: ";
    //public void SendMail()
    //{
    //    using (var intentClass = new AndroidJavaClass("android.content.Intent"))
    //    {
    //        using (var intentObject = new AndroidJavaObject("android.content.Intent", intentClass.GetStatic<string>("ACTION_SENDTO")))
    //        {
    //            //
    //            var uriClass = new AndroidJavaClass("android.net.Uri");
    //            var uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "mailto:");
    //            intentObject.Call<AndroidJavaObject>("setData", uriObject);

    //            // 
    //            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
    //            //yourmail
    //            string[] email = { "ichishka97@gmail.com" };
    //            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_EMAIL"), email);

    //            // emailBody
    //            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
    //            using (var unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
    //            {
    //                using (var currentActivity = unity.GetStatic<AndroidJavaClass>("currentActivity"))
    //                {
    //                    currentActivity.Call("startActivity", intentObject);
    //                }
    //            }
    //        }
    //    }
    //}

    string toEmail = "shch.dima@yandex.ru";
    public void sendEmail()
    {
        var subjectEsc = System.Uri.EscapeUriString(subject);
        var bodyEsc = System.Uri.EscapeUriString(body);
        Application.OpenURL("mailto:" + toEmail + "?subject=" + subjectEsc + "&body=" + bodyEsc);
    }
}

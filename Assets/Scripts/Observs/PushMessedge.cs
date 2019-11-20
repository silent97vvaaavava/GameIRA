using UnityEngine;
using NotificationSamples;
using System;


public class PushMessedge : MonoBehaviour
{
    [SerializeField] private GameNotificationsManager notificationManager;
    private int notificationDelay;


    private void Start()
    {
        InitializeNotifications();
    }

    public void OnTimeInput(string text)
    {
        if(int.TryParse(text, out int sec))
        {
            notificationDelay = sec;
        }
    }

    private void InitializeNotifications()
    {
        GameNotificationChannel channel = new GameNotificationChannel("getbonus", "Box!", "Receive bonus");
        notificationManager.Initialize(channel);
    }

    private void CreateNotification(string title, string body, DateTime time)
    {
        IGameNotification notification = notificationManager.CreateNotification();
        if(notification != null)
        {
            notification.Title = title;
            notification.Body = body;
            notification.DeliveryTime = time;
            notificationManager.ScheduleNotification(notification);
        }
    }

    public void pushMes()
    {
        CreateNotification("Я тут", "hi gyu", DateTime.Now.AddSeconds(1));
    }
}

using Common.Caching.Abstraction;
using Common.Caching.Redis;
using Common.Utilities.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using Web.Topcourse.Models;

namespace Web.Topcourse.Helper
{
    public class CourseViewCacheInit
    {
        static CourseViewCacheInit()
        {
           
        }

        private static ICourseViewCache _CourseView;
        public static ICourseViewCache CreateCourseViewCache()
        {
            if (_CourseView != null)
                return _CourseView;


            if (_CourseView == null)
            {
                IRedisClient client = new RedisClient(ConfigurationManager.AppSettings["RedisConnectString"]);
                _CourseView = new CourseViewCache(client, TimeSpan.FromDays(10 * 365));
            }
            return _CourseView;
        }
    }

    public class CourseViewCache : ICourseViewCache
    {
        internal static readonly string DefaultPrefix = "CacheView";
        private readonly IRedisClient _redisClient;
        private readonly TimeSpan _defaultExpire;
        internal CourseViewCache(IRedisClient redisClient, TimeSpan expire)
        {
            if (redisClient == null || expire == null)
            {
                throw new ArgumentNullException("redisClient and expireTimeSpan must not be null");
            }

            _redisClient = redisClient;
            _defaultExpire = expire;
        }
        public bool CheckConnection()
        {
            TimeSpan timespan = TimeSpan.FromMinutes(2);
            return _redisClient.Add("PingPong", "IsSuccess", DateTime.Now + timespan);
        }

        public bool AddOrUpdate(CacheCourseView report)
        {
            if (report == null)
            {
                throw new ArgumentNullException("icloud is invalid");
            }

            return _redisClient.Add(GetPrefix(report.UserID), report);
        }
        public CacheCourseView GetRePortModel(int userID)
        {
            return _redisClient.Get<CacheCourseView>(GetPrefix(userID));

        }
        public void Remove(int AccountID, string extend)
        {
            try
            {
                _redisClient.Remove(GetPrefix(AccountID));
            }
            catch (Exception) { }
        }
        private static string GetPrefix(long id)
        {
            return string.Format("{0}:{1}", DefaultPrefix, id);
        }
    }

    public interface ICourseViewCache
    {
        bool AddOrUpdate(CacheCourseView report);
        CacheCourseView GetRePortModel(int userID);
        void Remove(int AccountID, string extend);

    }

    public class CacheService : ICacheService
    {
        private static ICourseViewCache _ILogService = CourseViewCacheInit.CreateCourseViewCache();
        public void AddOrUpdate(int userID, CourseViewItem log)
        {

            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    var _log = this.GetLog(userID);
                    if (_log == null)
                        _log = new CacheCourseView { UserID= userID, Logs = new List<CourseViewItem>() };
                    _log.Logs.Add(log);

                    //Nếu vượt quá 5 giao dịch thfi loại bỏ cái đầu tiên là cái cũ nhất
                    if (_log.Logs.Count > 5)
                        _log.Logs.RemoveAt(0);

                    _ILogService.AddOrUpdate(_log);
                }
                catch (Exception ex)
                {

                    NLogManager.PublishException(ex);

                }
            });
        }

        public CacheCourseView GetLog(int userID)
        {
            return _ILogService.GetRePortModel(userID);
        }
    }

    public interface ICacheService
    {
        void AddOrUpdate(int userID, CourseViewItem log);
        CacheCourseView GetLog(int userID);

    }
}
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using OfficeLoginWebApp.Data;
using OfficeLoginWebApp.Models;
using System.Net;
using System.Net.Sockets;

namespace OfficeLoginWebApp.Services
{
    public class DataService : IDataService
    {
        public readonly LoggerContext _context;
        public DataService(LoggerContext context)
        {
            _context = context;
        }
        public void LoginEntry(string userId, string IPAddress)
        {
            OfficeLogger officeLogger = new OfficeLogger();
            //This code takes Local IP address and Save as string.
            string ipAddress = Dns.GetHostEntry(Dns.GetHostName())
            .AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
            .ToString();

            var firstName = string.Empty;
            var lastName = string.Empty;

            if (!String.IsNullOrEmpty(userId))
            {
                var userIdArr = userId.Split('@');
                userIdArr = userIdArr[0].Split('.');
                firstName = userIdArr[0];
                if (userIdArr.Length > 2)
                {
                    lastName = userIdArr[1] + " " + userIdArr[2];
                }
                else if (userIdArr.Length > 1)
                {
                    lastName = userIdArr[1];
                }
            }

            //ipAddress = Request.HttpContext.Connection.LocalIpAddress.ToString();

            //This code stores the value of user to our local database.
            officeLogger.LoggerEmail = userId;
            officeLogger.FirstName = Char.ToUpper(firstName[0]) + firstName.Substring(1);
            officeLogger.LastName = Char.ToUpper(lastName[0]) + lastName.Substring(1);
            officeLogger.IPAddress = IPAddress;
            officeLogger.IPAddressLocal = ipAddress;


            // Code to check whether the person is present in database or not.

            //var status = _context.officeLoggers.Where(x => x.LoggerEmail == officeLogger.LoggerEmail && x.LogginTime.Date == officeLogger.LogginTime.Date).ToList();

            var status = (from x in _context.officeLoggers
                          where x.LoggerEmail == officeLogger.LoggerEmail
                          && x.LoginTime.Date == officeLogger.LoginTime.Date
                          select x).ToList();

            //Checks and save data in respective database.
            if (status.Count > 0)
            {
                UserSigninLog userSigninLog = new UserSigninLog();
                userSigninLog.LoggerEmail = userId;
                userSigninLog.FirstName = Char.ToUpper(firstName[0]) + firstName.Substring(1);
                userSigninLog.LastName = Char.ToUpper(lastName[0]) + lastName.Substring(1);
                userSigninLog.IPAddress = IPAddress;
                userSigninLog.IPAddressLocal = ipAddress;

                _context.Add(userSigninLog);
            }
            else
            {
                _context.Add(officeLogger);
            }

            _context.SaveChanges();

        }

        public void logoutEntry(string userName)
        {

            OfficeLogger officeLogger = new OfficeLogger();
            UserSigninLog userSigninLog = new UserSigninLog();
            var logedEmail = userName;
            var today = DateTime.UtcNow.Date;

            var OLogToUpdate = _context.officeLoggers.Where(o => o.LoginTime.Date == today && o.LoggerEmail == logedEmail).FirstOrDefault();

            //await _context.userSigninLogs.OrderBy(u => u.UserId).LastOrDefaultAsync(u => u.LoggerEmail == logedEmail);

            if (OLogToUpdate.LogoutTime == null)
            {
                OLogToUpdate.LogoutTime = DateTime.UtcNow;
                _context.Update(OLogToUpdate);
            }

            else
            {
                var MaxUserId = _context.userSigninLogs.Where(u => u.LoginTime.Date == today && u.LoggerEmail == logedEmail).Max(u => u.UserId);
                var ULogToUpdate = _context.userSigninLogs.Where(u => u.UserId == MaxUserId).FirstOrDefault();


                ULogToUpdate.LogoutTime = DateTime.UtcNow;
                _context.Update(ULogToUpdate);
            }

            _context.SaveChanges();
        }
    }
}

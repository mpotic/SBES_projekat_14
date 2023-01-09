﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogger
{
	public class Audit : IDisposable
	{

		private static EventLog customLog = null;
		const string SourceName = "CustomLogger.Audit";
		const string LogName = "SBESProject";

		static Audit()
		{
			try
			{
				if (!EventLog.SourceExists(SourceName))
				{
					EventLog.CreateEventSource(SourceName, LogName);
				}
				customLog = new EventLog(LogName,
					Environment.MachineName, SourceName);
			}
			catch (Exception e)
			{
				customLog = null;
				Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
			}
		}

		public static void ReplicationSuccess(string service, string subjectName)
		{
			if (customLog != null)
			{
				try
				{
					string ReplicationSuccess =
						AuditEvents.ReplicationSuccess;
					string message = String.Format(ReplicationSuccess, service, subjectName);
					customLog.WriteEntry(message);
				}
				catch (Exception e)
				{
					Console.WriteLine((string.Format("Error while trying to write event (eventid = {0}) to event log.",
						(int)AuditEventTypes.ReplicationSuccess)), "\nException", e.Message);
				}
			}
		}

		public static void ReplicationFailure(string service, string subjectName)
		{
			if (customLog != null)
			{
				try
				{
					string ReplicationFailure =
						AuditEvents.ReplicationFailure;
					string message = String.Format(ReplicationFailure, service, subjectName);
					customLog.WriteEntry(message, EventLogEntryType.Error);
				}
				catch(Exception e)
				{
					Console.WriteLine((string.Format("Error while trying to write event (eventid = {0}) to event log.",
						(int)AuditEventTypes.ReplicationFailure)), "\nException", e.Message);
				}
			}
		}

		public static void PinReplicationSuccess(string service, string subjectName)
		{
			if (customLog != null)
			{
				try
				{
					string PinReplicationSuccess =
						AuditEvents.PinReplicationSuccess;
					string message = String.Format(PinReplicationSuccess, service, subjectName);
					customLog.WriteEntry(message);
				}
				catch(Exception e)
				{
					Console.WriteLine((string.Format("Error while trying to write event (eventid = {0}) to event log.",
						(int)AuditEventTypes.PinReplicationSuccess)), "\nException", e.Message);
				}
			}
		}
		public static void PinReplicationFailure(string service, string subjectName)
		{
			if (customLog != null)
			{
				try
				{
					string PinReplicationFailure =
						AuditEvents.PinReplicationFailure;
					string message = String.Format(PinReplicationFailure, service, subjectName);
					customLog.WriteEntry(message, EventLogEntryType.Error);
				}
				catch (Exception e)
				{
					Console.WriteLine((string.Format("Error while trying to write event (eventid = {0}) to event log.",
						(int)AuditEventTypes.PinReplicationFailure)), "\nException", e.Message);
				}
			}
		}

		public static void PinChangeSuccess(string service, string subjectName)
		{
			if (customLog != null)
			{
				try
				{
					string PinChangeSuccess =
						AuditEvents.PinChangeSuccess;
					string message = String.Format(PinChangeSuccess, service, subjectName);
					customLog.WriteEntry(message);
				}
				catch (Exception e)
				{
					Console.WriteLine((string.Format("Error while trying to write event (eventid = {0}) to event log.",
						(int)AuditEventTypes.PinChangeSuccess)), "\nException", e.Message);
				}
			}
		}

		public static void PinChangeFailure(string service, string subjectName)
		{
			if (customLog != null)
			{
				try
				{
					string PinChangeFailure =
						AuditEvents.PinChangeFailure;
					string message = String.Format(PinChangeFailure, service, subjectName);
					customLog.WriteEntry(message, EventLogEntryType.Error);
				}
				catch (Exception e)
				{
					Console.WriteLine((string.Format("Error while trying to write event (eventid = {0}) to event log.",
						(int)AuditEventTypes.PinChangeFailure)), "\nException", e.Message);
				}
			}
		}


		public void Dispose()
		{
			if (customLog != null)
			{
				customLog.Dispose();
				customLog = null;
			}
		}
	}
}

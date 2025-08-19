using CustomCADs.Speedy.Core.Contracts.Track;

namespace CustomCADs.Speedy.Core.Services;

public static class TrackCodeTranslator
{
	public static string Translate(this TrackedParcelOperationModel operation)
		=> operation.OperationCode switch
		{
			1 => "Arrival Scan",
			2 => "Departure Scan",
			11 => "Received in Office",
			12 => "Out for Delivery",
			-14 => "Delivered",
			21 => "Processed in Office",
			38 => "Returned to Office",
			39 => "Courier Pick-up",
			44 => "Unsuccessful Delivery",
			69 => "Deferred delivery",
			111 => "Return to Sender",
			112 => "Processed to the Insurance dept.",
			114 => "Processed for Destruction",
			115 => "Redirected",
			116 => "Forwarded",
			121 => "Stopped by Sender",
			123 => "Refused by recipient",
			124 => "Delivered Back to Sender",
			125 => "Destroyed",
			127 => "Theft/Burglary",
			128 => "Canceled",
			129 => "Administrative Closure",
			134 => "Prepared for Self-collecting Consignee",
			144 => "Handover for contents check/test",
			148 => "Shipment data received",
			164 => "Unsuccessful shipment pickup",
			169 => "Delivery capacity reached (tour/office)",
			175 => "Predict",
			176 => "Export to foreign provider",
			181 => "Unexpected delay",
			190 => "Postponed delivery due to inaccurate/incomplete address",
			195 => "Refuse contents check/test",
			217 => "Handover to midway carrier",
			1134 => "Office/Locker ready for pickup message sent",
			_ => "Unkown Status",
		};
}

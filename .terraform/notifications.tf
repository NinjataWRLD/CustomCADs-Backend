# Simple Notification Service
resource "aws_sns_topic" "customcads_notification" {
  name = "customcads-health-notifications"
}
# Role
resource "aws_iam_role" "customcads_eb_service_role" {
  name = "AWSElasticBeanstalkServiceRole"

  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Effect = "Allow"
        Principal = {
          Service = "elasticbeanstalk.amazonaws.com"
        }
        Action = "sts:AssumeRole"
      }
    ]
  })
}

# Policies
resource "aws_iam_role_policy_attachment" "customcads_eb_policy_managed_updates" {
  role       = aws_iam_role.customcads_eb_service_role.name
  policy_arn = "arn:aws:iam::aws:policy/AWSElasticBeanstalkManagedUpdatesCustomerRolePolicy"
}
resource "aws_iam_role_policy_attachment" "customcads_eb_policy_enhanced_health" {
  role       = aws_iam_role.customcads_eb_service_role.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AWSElasticBeanstalkEnhancedHealth"
}

# Instance Profile
resource "aws_iam_instance_profile" "customcads_instance_profile" {
  name = "customcads-instance-profile"
  role = aws_iam_role.customcads_eb_service_role.name
}

# Policy Documents
data "aws_iam_policy_document" "customcads_eb_service_role_policy_doc" {
  statement {
    effect = "Allow"
    principals {
      type        = "Service"
      identifiers = ["elasticbeanstalk.amazonaws.com"]
    }
    actions = ["sts:AssumeRole"]
  }
}
data "aws_iam_policy_document" "customcads_eb_instance_profile_policy_doc" {
  statement {
    effect = "Allow"
    principals {
      type        = "Service"
      identifiers = ["ec2.amazonaws.com"]
    }
    actions = ["sts:AssumeRole"]
  }
}

# Role
resource "aws_iam_role" "customcads_eb_service_role" {
  name               = "customcads-eb-service-role"
  assume_role_policy = data.aws_iam_policy_document.customcads_eb_service_role_policy_doc.json
}
resource "aws_iam_role" "customcads_eb_instance_role" {
  name               = "customcads-eb-instance-role"
  assume_role_policy = data.aws_iam_policy_document.customcads_eb_instance_profile_policy_doc.json
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
  role = aws_iam_role.customcads_eb_instance_role.name
}

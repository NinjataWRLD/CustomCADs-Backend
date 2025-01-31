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

# Roles
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
resource "aws_iam_role_policy_attachment" "customcads_eb_policy_sns_access" {
  role       = aws_iam_role.customcads_eb_service_role.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonSNSFullAccess"
}
resource "aws_iam_role_policy_attachment" "customcads_eb_policy_ec2_full_access" {
  role       = aws_iam_role.customcads_eb_service_role.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonEC2FullAccess"
}
resource "aws_iam_role_policy_attachment" "customcads_eb_policy_ecr_access" {
  role       = aws_iam_role.customcads_eb_instance_role.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonEC2ContainerRegistryFullAccess"
}
resource "aws_iam_role_policy_attachment" "customcads_eb_policy_ec2_access" {
  role       = aws_iam_role.customcads_eb_instance_role.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonEC2FullAccess"
}
resource "aws_iam_role_policy_attachment" "customcads_eb_policy_eb_access" {
  role       = aws_iam_role.customcads_eb_instance_role.name
  policy_arn = "arn:aws:iam::aws:policy/AdministratorAccess-AWSElasticBeanstalk"
}
resource "aws_iam_role_policy_attachment" "customcads_eb_policy_s3_access" {
  role       = aws_iam_role.customcads_eb_instance_role.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonS3FullAccess"
}
resource "aws_iam_role_policy_attachment" "customcads_eb_policy_rds_access" {
  role       = aws_iam_role.customcads_eb_instance_role.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonRDSFullAccess"
}
resource "aws_iam_role_policy_attachment" "customcads_eb_policy_cw_access" {
  role       = aws_iam_role.customcads_eb_instance_role.name
  policy_arn = "arn:aws:iam::aws:policy/CloudWatchLogsFullAccess"
}

# Instance Profile
resource "aws_iam_instance_profile" "customcads_instance_profile" {
  name = "customcads-instance-profile"
  role = aws_iam_role.customcads_eb_instance_role.name
}

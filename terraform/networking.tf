# Virtual Private Cloud
resource "aws_vpc" "customcads_vpc" {
  cidr_block           = "10.0.0.0/16"
  enable_dns_hostnames = true
  enable_dns_support   = true
}

# Security Groups
resource "aws_security_group" "customcads_db_security_group" {
  vpc_id      = aws_vpc.customcads_vpc.id
  name        = "customcads-rds-sg"
  description = "CustomCADs Database SG"

  ingress = [
    {
      description      = "Allow SQL Server access from VPC"
      protocol         = "tcp"
      cidr_blocks      = ["10.0.0.0/16"]
      to_port          = 5432
      from_port        = 5432
      ipv6_cidr_blocks = []
      prefix_list_ids  = []
      security_groups  = []
      self             = false
    }
  ]
}
resource "aws_security_group" "customcads_app_security_group" {
  vpc_id      = aws_vpc.customcads_vpc.id
  name        = "customcads-eb-sg"
  description = "CustomCADs Application SG"

  ingress = [
    {
      description      = "Allow access to all (HTTPS)"
      protocol         = "tcp"
      cidr_blocks      = ["0.0.0.0/0"]
      to_port          = 443
      from_port        = 443
      ipv6_cidr_blocks = []
      prefix_list_ids  = []
      security_groups  = []
      self             = false
    },
    {
      description      = "Allow access to all (HTTP)"
      protocol         = "tcp"
      cidr_blocks      = ["0.0.0.0/0"]
      to_port          = 80
      from_port        = 80
      ipv6_cidr_blocks = []
      prefix_list_ids  = []
      security_groups  = []
      self             = false
    }
  ]

  egress = [
    {
      description      = "Allow access to all"
      protocol         = "-1"
      cidr_blocks      = ["0.0.0.0/0"]
      to_port          = 0
      from_port        = 0
      ipv6_cidr_blocks = []
      prefix_list_ids  = []
      security_groups  = []
      self             = false
    }
  ]
}

# Subnets
resource "aws_subnet" "customcads_subnet_public1_a" {
  vpc_id            = aws_vpc.customcads_vpc.id
  cidr_block        = "10.0.0.0/20"
  availability_zone = "us-east-1a"

  tags = {
    Name = "customcads-subnet-public1-us-east-1a"
  }
}
resource "aws_subnet" "customcads_subnet_public2_b" {
  vpc_id            = aws_vpc.customcads_vpc.id
  cidr_block        = "10.0.16.0/20"
  availability_zone = "us-east-1b"

  tags = {
    Name = "customcads-subnet-public2-us-east-1b"
  }
}
resource "aws_subnet" "customcads_subnet_public3_c" {
  vpc_id            = aws_vpc.customcads_vpc.id
  cidr_block        = "10.0.32.0/20"
  availability_zone = "us-east-1c"

  tags = {
    Name = "customcads-subnet-public3-us-east-1c"
  }
}
resource "aws_subnet" "customcads_subnet_private1_a" {
  vpc_id            = aws_vpc.customcads_vpc.id
  cidr_block        = "10.0.128.0/20"
  availability_zone = "us-east-1a"

  tags = {
    Name = "customcads-subnet-private1-us-east-1a"
  }
}
resource "aws_subnet" "customcads_subnet_private2_b" {
  vpc_id            = aws_vpc.customcads_vpc.id
  cidr_block        = "10.0.144.0/20"
  availability_zone = "us-east-1b"

  tags = {
    Name = "customcads-subnet-private2-us-east-1b"
  }
}
resource "aws_subnet" "customcads_subnet_private3_c" {
  vpc_id            = aws_vpc.customcads_vpc.id
  cidr_block        = "10.0.160.0/20"
  availability_zone = "us-east-1c"

  tags = {
    Name = "customcads-subnet-private3-us-east-1c"
  }
}

# Subnet Group
resource "aws_db_subnet_group" "customcads_subnet_group" {
  name = "customcads-subnet-group"
  subnet_ids = [
    aws_subnet.customcads_subnet_public1_a.id,
    aws_subnet.customcads_subnet_public2_b.id,
    aws_subnet.customcads_subnet_public3_c.id,
    aws_subnet.customcads_subnet_private1_a.id,
    aws_subnet.customcads_subnet_private2_b.id,
    aws_subnet.customcads_subnet_private3_c.id
  ]
}
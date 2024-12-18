# Virtual Private Cloud
resource "aws_vpc" "customcads_vpc" {
  cidr_block           = "10.0.0.0/16"
  enable_dns_hostnames = true
  enable_dns_support   = true

  tags = {
    Name = "customcads-vpc"
  }
}

# Internet Gateway
resource "aws_internet_gateway" "customcads_internet_gateway" {
  vpc_id = aws_vpc.customcads_vpc.id
}

# Route Tables
resource "aws_route_table" "customcads_route_table" {
  vpc_id = aws_vpc.customcads_vpc.id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.customcads_internet_gateway.id
  }

  tags = {
    Name = "customcads-route-table"
  }
}
resource "aws_route_table_association" "customcads_route_table_association" {
  count          = 3
  subnet_id      = element([aws_subnet.customcads_subnet_public1_a.id, aws_subnet.customcads_subnet_public2_b.id, aws_subnet.customcads_subnet_public3_c.id], count.index)
  route_table_id = aws_route_table.customcads_route_table.id
}

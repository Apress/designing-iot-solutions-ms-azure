﻿# =====================================================================================
# PowerShell File WITH CLI Commands
# NAME: Azure SQL Server Setup.ps1
# AUTHOR: Nirnay Bansal
# DATE  : 6/20/2020
# 
# COMMENT: Create Azure SQL Server
#
# DISCLAIMER AND WARNING:
# 	This Sample Code is unsigned and is provided for the purpose of illustration 
# 	only and is not intended to be used in a production environment. THIS SAMPLE 
# 	CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
# 	KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
# 	WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. We grant 
# 	You a nonexclusive, royalty-free right to use and modify the Sample Code and to
# 	reproduce and distribute the object code form of the Sample Code, provided
# 	that You agree: (i) to not use my name, logo, or contact info to market Your
# 	software product in which the Sample Code is embedded; (ii) to indemnify, 
#   hold harmless, and defend Us and Our suppliers from and against any claims 
# 	or lawsuits, including attorneys’ fees, that arise or result from the use 
# 	or distribution of this Sample Code.
#
#   RUNNING THIS SCRIPT MAY BILL YOU SUBSTANTIAL COST BEYOND IMAGINATION
#   ON YOUR AZURE SUBSCRIPTION.
#
#   IF YOU DON'T KNOW WHAT THIS SCRIPT WILL DO, DO NOT RUN IT! 
# ====================================================================================

#Install the Azure CLI to execute following commands  through  Windows Command Prompt (CMD) or PowerShell
#https://aka.ms/installazurecliwindows

#Sign in with Azure CLI
az login

#Set a subscription to be the current active subscription
az account set --subscription "<Your Subscription id>"

#Create a resource group to manage all the resources
az group create --name rgIot --location westus

#Create a server
az sql server create --location westus --resource-group rgIot --name supplychain --admin-user adminuser --admin-password adminpassword

Create a Standard S0 database
az sql db create --resource-group mygroup --server supplychain -n dbrfid --service-objective S0

#Congratulation! Your SQL database is ready.
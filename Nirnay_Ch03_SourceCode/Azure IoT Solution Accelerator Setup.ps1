# =====================================================================================
# PowerShell File WITH CLI Commands
# NAME: Azure IoT Solution Accelerator Setup.ps1
# AUTHOR: Nirnay Bansal
# DATE  : 6/20/2020
# 
# COMMENT: Create IoT Solution Acclerator
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

#To execute following commands, you need Node.js installed on your local machine
#https://nodejs.org/en/download/

#You need to add the IoT Solutions node.js package to enable additional functionality
npm install iot-solutions -g

#Execute following command to get Authentication code and paste that code at https://microsoft.com/devicelogin to complete login process
pcs login

#Following command will asked for:
#   A unique name for your solution. Put 
#   The Azure subscription Id. Put "<Your Subscription id>"
#   A location. Put westus
#   Credentials for the virtual machines that solution will create. Put user id and strong password
#Note: This command may take upto 1 hour depends on location you choose.
pcs -t remotemonitoring -s basic -r dotnet

#Congratulation! Your first IoT Solution Acclerator is ready.
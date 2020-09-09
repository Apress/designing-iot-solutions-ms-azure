# =====================================================================================
# PowerShell File WITH CLI Commands
# NAME: Device Management.ps1
# AUTHOR: Nirnay Bansal
# DATE  : 6/20/2020
# 
# COMMENT: Device Management
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

#Prerequisites
#     To execute following command, you need to run "Azure IoT Hub Setup.ps1"

#Create a device in an IoT Hub.
az iot hub device-identity create -n myfirstiothub -d mySecondDevice 

#Congratulation! Your second device is registered.


#Sending structured and unstructured telemetry data
    #Device-to-Cloud (D2C) Message
    az iot device send-d2c-message -n myfirstiothub -d mySecondDevice --data 'temperature=30.3'

    #Upload a local file as a device to a pre-configured blob storage container.
    az iot device upload-file --hub-name myfirstiothub --device-id mySecondDevice --content-type 'image/jpeg' --file-path '.\images.jpeg' --debug

#Congratulation! You sent D2C structured and unstructured data to your IoT hub from a registered device.
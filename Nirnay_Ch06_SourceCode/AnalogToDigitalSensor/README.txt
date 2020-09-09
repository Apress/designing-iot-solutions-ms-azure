//Open "x86_x64 Cross Tools Command Prompt for VS 2017" Command prompt

MakeAppx pack /d "<your local path>\bin\ARM\Debug" /p "<your local path>\outpout\AnalogToDigitalSensor" /h SHA256

signtool sign /a /v /fd SHA256 /f "<your local path>\AnalogToDigitalSensor_TemporaryKey.pfx" "<your local path>\outpout\AnalogToDigitalSensor.appx"
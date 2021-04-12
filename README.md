sendgridjp-csharp-example
=========================

本コードは[SendGrid公式C#ライブラリ](https://github.com/sendgrid/sendgrid-csharp)の利用サンプルです。

## 使い方

```bash
git clone git@github.com:SendGridJP/sendgridjp-csharp-example.git
cd sendgridjp-csharp-example
cp .env.example .env
```

## .envファイルの編集
.envファイルは以下のような内容になっています。

```
API_KEY=api_key
TOS=you@youremail.com,friend1@friendemail.com,friend2@friendemail.com
FROM=you@youremail.com
```
API_KEY:SendGridの[API Key](https://sendgrid.kke.co.jp/docs/User_Manual_JP/Settings/api_keys.html)を指定してください。  
TOS:宛先をカンマ区切りで指定してください。  
FROM:送信元アドレスを指定してください。  

import smtplib
from email.mime.text import MIMEText
from email.mime.multipart import MIMEMultipart

# Dateien_Einlesen
Pass = open("/usr/bin/p.txt", "r")
txt = Pass.readlines()
Pass.close()
Datei = open("/usr/bin/check.txt", "r")
Text = Datei.readlines()
Datei.close()

# Texte_Filtern
for Satz in Text:
Satz.split('\n')

# Passwort_Filtern
for pw in txt:
pw.split('\n')

# Definition_Email
email = 'hausarbeit20192020@xxx'
pwd = pw
send_to_mail = 'test@yahoo.com'
subject = 'WARNUNG!'
message = Satz

msg = MIMEMultipart()
msg['From'] = email
msg['To'] = send_to_mail
msg['Subject'] = subject

msg.attach(MIMEText (message, 'plain'))

def sendeAlarmNachricht():
try:
    server = smtplib.SMTP("smtp.gmail.com", 587)
    server.starttls()
    server.login(email, pw)
    text = msg.as_string()
    server.sendmail(email, send_to_mail, text)
    server.quit()
    print ("Die E-Mail wurde versendet!")
except:
    print ("Die E-Mail wurde nicht versendet!")

sendeAlarmNachricht()

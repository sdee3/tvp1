# Visoka škola elektrotehnike i računarstva strukovnih studija Beograd
# Tehnike vizuelnog programiranja - Prvi projekat (2017/2018)
Koristeći Visual C# napisati Windows Form aplikaciju koja će predstavljati pojednostavljen sistem za upis studenata na fakultet. Aplikacija treba da se sastoji iz dva dela, a korisnik na početku bira željeni deo:

## 1. Prvi deo, Administracija obuhvata ažuriranje sledećih podataka:
a) Smer. Podaci o Smeru (jedinstveni celobrojni identifikator, naziv).

b) Student. Podaci o Studentu (jedinstveni identifikator indeks, ime, prezime, jmbg, datum rođenja, telefon, smer).

c) Predmet. Podaci o Predmetu (jedinstveni celobrojni identifikator (šifra), naziv, smer, profesor, espb, obavezan, semestar).

d) Izborna lista. Podaci o Izbornim listama (student, predmeti).

e) Statistika studenata po predmetu. Brojčano i procentualno koristeći crtanje prikazuje se statistika odabranog predmeta.

## 2. Drugi deo, Student:
a) Nakon prijave, studentu se prikazuje lista predmeta za njegov smer i mogućnost odabira predmeta.

b) Postoji i padajuća lista koja sadrži predmete sa drugog smera.

c) Postoji i neizmenljivo polje koje računa broj espb bodova za odabrane predmete.

d) Nakon uspešnog slanja liste, Student se preusmerava na početnu formu aplikacije.

### Napomena:
Svi podaci se čuvaju u datotekama.

### Administrativni deo:
Deo vezan za Izborne liste se menja tako što se prvo izabere student iz padajućeg menija i prikažu svi predmeti za njega. Delovi u kojima se pominje ažuriranje sadrže upis, izmenu i brisanje za izabranu kategoriju.

### Korisnički deo:
Prilikom prikaza predmeta, predmeti su sortirani po semestru a zatim po nazivu. Za svaki prikazan predmet sa matičnog smera dinamički se kreira CheckBox kontrola za svaki predmet. Student mora da ima najmanje 48 bodova kako bi predao izbornu listu. U slučaju da je predmet obavezan, Studentu je CheckBox za taj predmet čekiran i onemogućena je promena. U slučaju da je student već popunio listu, nakon prijave, predmeti koji su prethnodno izabrani su označeni i Studentu se daje mogućnost izmene te liste. Moguće je izabrati samo jedan predmet sa drugog smera. Pre preusmeravanja na početnu formu, Student se automatski odjavljuje.

Vrednuje se odbrana projekta sa naglaskom na:
1. Adekvatno objašnjenje napisanog koda
2. Kompletnost rešenja
3. Ispravnost u radu aplikacije
4. Kvalitet sa stanovišta daljeg održavanja

# APBD-cw5
Aplikacja REST API,
która umożliwia wykonanie operacji pozwalających na modyfikowanie danych
w bazie SQL Server.
Komunikacja z bazą danych powinna
odbywa się poprzez klasy SqlConnection/SqlCommand.
1. Dodaj kontroler Animals
2. Dodaj metodę/endpoint pozwalającą na uzyskanie listy zwierząt.
Końcówka powinna reagować na żądanie typu HTTP GET wysłane na
adres /api/animals
1. Końcówka powinna pozwolić na przyjęcie parametru w query string,
który określa sortowanie. Parametr nazywa się orderBy. Przykład:
api/animals?orderBy=name
2. Parametr jako dostępne wartości przyjmuje: name, description,
category, area. Możemy sortować wyłącznie po jednej kolumnie.
Sortowanie jest zawsze w kierunku „ascending”.
3. Domyślne sortowanie (kiedy w żądaniu nie zostanie przekazany
parametr w query string) powinna odbywać się po kolumnie name.
3. Dodaj metodę/endpoint pozwalający na dodanie nowego zwierzęcia.
1. Metoda powinna odpowiadać na żądanie HTTP POST na adres
api/animals
2. Metoda powinna przyjmować dane w postaci JSON2
4. Dodaj metodę/endpoint pozwalający na aktualizację danych konkretnego
zwierzęcia.
1. Metoda powinna odpowiadać na żądanie HTTP PUT wysłane na
adres /api/animals/{idAnimal}
2. Metoda przyjmuje dane w postaci JSON’a
3. Zakładamy, że klucze główne nie ulegają modyfikacji (kolumna
IdAnimal)
5. Dodaj metodę/endpoint do usuwania danych na temat konkretnego
zwierzęcia.
1. Metoda powinna odpowiadać na żądanie HTTP DELETE wysłane
na adres /api/animals/{idAnimal}
6. Pamiętaj o poprawnych kodach HTTP
7. Postaraj się skorzystać z wbudowanego mechanizmu do
DependencyInjection.
8. Dbaj o walidację danych
9. Dbaj o nazewnictwo i sty
/*--DADES--*/
///////////////////////////////////////////////////////////////////////////

restart: //Punt de referencia en al codi per a tornar a començar el joc.
Console.Title = "Hangman";
Random numgen = new Random();

int numeroVides = 7;  //Inicialitza un int el qual fem servir per a seguir el nombre d'intents restants.
int numeroEncerts = 0; //Inicialitza un int el qual fem servir per a seguir el nombre de lletres encertades.

char[,] abecedari = new char[3, 9] { { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' },  //Array amb les lletres.
                                     { 'J', 'K', 'L', 'M', 'N', 'Ç', 'O', 'P', 'Q' },   
                                     { 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' } };

int[,] checker = new int[3, 9] { { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //Array que serveix per a verificar quines lletres s'han escrit.
                                 { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                 { 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

string[] paraules = new string[14] {"AUDIOVISUAL", "MULTIMEDIA", "IMATGE", "DIBUIX", "CAMERA" , "EDITOR", "DIRECTOR", "PRODUCTOR", "DIBUIXANT", "ANIMADOR", "LLUM", "OBJECTIU", "MEMORIA", "LLAPIS" }; //Array que permet escriure les possibles paraules.
int encertar = numgen.Next(0, 14); //Generador de nombres aleatoris.                                            
string paraulaAEncertar = paraules[encertar]; //String que permet emmagatzemar la paraula possible escollida gracies als nombres aleatoris.

//Crea un array de chars que servira per a mostrar les lletres que te la paraula i anar mostrant-la quan es descobreixin les lletres.
char[] paraulaACompletar = new char[paraulaAEncertar.Length];
for (int i = 0; i < paraulaAEncertar.Length; i++)
{
    paraulaACompletar[i] = '_';
}

char lletraEscollida = ' '; //Inicialitza un char en blanc que s'utilitzara com el que el jugador entrarà per teclat.

/*--FUNCIONS--*/
///////////////////////////////////////////////////////////////////////////
static void imprimirBienvenida(ref int dificultat) //Funcio que mostra el menu d'inici i la selecció de dificultat.
{
    int rules = 0;
    benvinguts:
    Console.Clear();
    Console.Title = "";
    Console.WriteLine();
    Console.WriteLine("*********************************************************");
    Console.WriteLine("*                                                       *");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("        ▄  █ ██      ▄     ▄▀  █▀▄▀█ ██      ▄   ");
    Console.WriteLine("       █   █ █ █      █  ▄▀    █ █ █ █ █      █  ");
    Console.WriteLine("       ██▀▀█ █▄▄█ ██   █ █ ▀▄  █ ▄ █ █▄▄█ ██   █ ");
    Console.WriteLine("       █   █ █  █ █ █  █ █   █ █   █ █  █ █ █  █ ");
    Console.WriteLine("          █     █ █  █ █  ███     █     █ █  █ █ ");
    Console.WriteLine("         ▀     █  █   ██         ▀     █  █   ██ ");
    Console.ResetColor();
    Console.WriteLine("*                                                       *");
    Console.WriteLine("*********************************************************");
    Console.WriteLine();
    if (rules == 0)
    {
        Console.WriteLine("  Per a jugar el joc, siusplau activa el BLOQ MAJUS");
        Console.WriteLine("  Prem qualsevol tecla per continuar. . .");
        Console.ReadKey();
        rules = 1;
        goto benvinguts;
    }
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("  Siusplau, escull un nivell de dificultat:");
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\t   1. Fàcil");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("\t   2. Normal");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\t   3. Difícil");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("\t   4. SUPREMA");
    Console.ResetColor();
    string nivell = "";
    Console.WriteLine();
    Console.Write("  ");
    string seleccioDificultat = Console.ReadLine();

    switch(seleccioDificultat) //Switch on depenent de la selecció de dificultat el jugador te més o menys intents.
    {
        case "1":
            dificultat = 7;
            nivell = "facil";
            break;
        case "FACIL":
            goto case "1";
        case "FÀCIL":
            goto case "1";

        case "2":
            dificultat = 5;
            nivell = "normal";
            break;
        case "NORMAL":
            goto case "2";

        case "3":
            dificultat = 4;
            nivell = "difícil";
            break;
        case "DIFICIL":
            goto case "3";
        case "DIFÍCIL":
            goto case "3";

        case "4":
            dificultat = 3;
            nivell = "suprema";
            break;
        case "SUPREMA":
            goto case "4";


        default:
            goto benvinguts;
    }
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("  Has escollit la dificultat " + nivell + ". Bona sort!");
    Console.WriteLine("  Prem qualsevol tecla per continuar. . .");
    Console.ReadKey();
}

static void imprimirNumeroVides(ref int vides) //Funcio que mostra el nombre de intents restants que te el jugador.
{
    Console.WriteLine("*********************************************************");
    Console.WriteLine("*                                                       *");
    Console.Write("\t     Número d'intents restants: ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(vides);
    Console.ResetColor();
    Console.WriteLine("*                                                       *");
    Console.WriteLine("*********************************************************");
    Console.WriteLine();
}

static void imprimirAbecedario(char abc, char select, ref int check) //Funcio que imprimeix per pantalla l'array d'Abecedari i la compara amb la de Check per a poder evitar la repetició 
{                                                                    //de lletres i poder indicar amb color les ja utilitzades.
    if (select == abc)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        check = 1;
    }
    if (check == 1) Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(abc + " ");
    Console.ResetColor();
}

static void imprimirPalabra(char lletra, char paraula, ref char endevinar) //Funció que mostra la paraula a endevinar, on les lletres no trobades son '_', i la va actualitzant amb les trobades.
{
    if (endevinar != '_') Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(endevinar + " ");
    Console.ResetColor();
}

static void imprimirDibujo(int vides) //Funcio que mostra les diferents possibilitats de dibuix de penjat segons el nombre restant d'intents.,
{
    if (vides == 0)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      _______                              ");
        Console.WriteLine("                      |     |                              ");
        Console.Write("                      |     ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("O");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("                      |    ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("/");
        Console.Write("|");
        Console.WriteLine("\\");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("                      |");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("     |");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("                     _|_");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("   / ");
        Console.WriteLine("\\");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                    // \\\\                                  ");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();
    }
    if (vides == 1)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      _______                              ");
        Console.WriteLine("                      |     |                              ");
        Console.Write("                      |     ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("O");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("                      |    ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("/");
        Console.Write("|");
        Console.WriteLine("\\");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("                      |");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("     |");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("                     _|_");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("   / ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                    // \\\\                                  ");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();
    }
    if (vides == 2)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      _______                              ");
        Console.WriteLine("                      |     |                              ");
        Console.Write("                      |     ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("O");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("                      |    ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("/");
        Console.Write("|");
        Console.WriteLine("\\");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("                      |");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("     |");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                     _|_");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                    // \\\\                                  ");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();
    }
    if (vides == 3)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      _______                              ");
        Console.WriteLine("                      |     |                              ");
        Console.Write("                      |     ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("O");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("                      |    ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("/");
        Console.Write("|");
        Console.WriteLine("\\");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      |");
        Console.WriteLine("                     _|_");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                    // \\\\                                  ");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();
    }
    if (vides == 4)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      _______                              ");
        Console.WriteLine("                      |     |                              ");
        Console.Write("                      |     ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("O");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("                      |    ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("/");
        Console.WriteLine("|");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      |");
        Console.WriteLine("                     _|_");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                    // \\\\                                  ");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();
    }
    if (vides == 5)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      _______                              ");
        Console.WriteLine("                      |     |                              ");
        Console.Write("                      |     ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("O");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("                      |    ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("/");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      |");
        Console.WriteLine("                     _|_");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                    // \\\\                                  ");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();
    }
    if (vides == 6)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      _______                              ");
        Console.WriteLine("                      |     |                              ");
        Console.Write("                      |     ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("O");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      |    ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      |");
        Console.WriteLine("                     _|_");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                    // \\\\                                  ");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();
    }
    if (vides == 7)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      _______                              ");
        Console.WriteLine("                      |     |                              ");
        Console.WriteLine("                      |     ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      |    ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                      |");
        Console.WriteLine("                     _|_");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                    // \\\\                                  ");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();
    }
}

static char pedirLetra() //Funció que permet demanar a l'usuari la lletra que es vol escollir i ho retorna al codi principal
{
    Console.WriteLine("*********************************************************");
    Console.Write("\t     Tecleja una lletra o vocal: ");
    char input = Convert.ToChar(Console.ReadLine());

    return input;
}

static void Lletrarepetida(char abecedari, char lletraEscollida, int check, ref int start) //Funcio que indica si s'ha escrit una lletra repetida i t'ho diu. 
{
    if (abecedari == lletraEscollida)
    {
        if (check == 1)
        {

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t    Lletra repetida, escull una alta");
            Console.ResetColor();
            Console.ReadKey();
            start = 1;
        }
    }
}

static void actualizarAciertos(char lletra, char paraula, ref char endevinar, ref int encerts, int encertscheck,    //Funcio que actualitza el contador d'encerts i actualitza la paraula 
                               ref int vides, int length, ref int count)                                            //a encertar amb la lletra corresponent si es endevinada, si no resta un intent per ronda.
{
    if (paraula == lletra)
                                                                                                                                                        
    {
        endevinar = paraula;
        encerts++;
    }

    count++; //Conta la lletra que es del bucle

    if ((count == (length-1)) && (encerts == encertscheck)) //Compara si el bucle es troba en la ultima lletra i si es el cas i no s'ha pogut trobar, resta una vida.
    {
        vides--;
    }
   
}



/*--CODI PRINCIPAL--*/
///////////////////////////////////////////////////////////////////////////

imprimirBienvenida(ref numeroVides); //Apareix en pantalla el menu d'inici creat en la funció.
while ((numeroVides >= 0)) //Bucle de joc que es mante mentres encara quedin intents.
{
    start: //Punt de referencia creat en cas de que es seleccioni una lletra ja dita.
    Console.Clear();
    imprimirNumeroVides(ref numeroVides);  //Apareix en pantalla el contador d'intents restants creat en la funció.
    for (int i = 0; i < 3; i++) //Es fa un bucle dins un altre per anar mostrant les lletres del abecedari en la pantalla tal com hem creat en la funció.
    {
        Console.Write("\t\t  ");
        for (int j = 0; j < 9; j++)
        {
            imprimirAbecedario(abecedari[i, j], lletraEscollida, ref checker[i, j]);
        }
        Console.WriteLine();
    }

    Console.WriteLine();
    Console.WriteLine();
    Console.Write("\t\t  ");
    for (int i = 0; i < paraulaAEncertar.Length; i++) //Es fa un bucle que mostra les lletres en blanc i les que han estat ja endevinades.
    { 
        imprimirPalabra(lletraEscollida, paraulaAEncertar[i], ref paraulaACompletar[i]);

    }
    Console.WriteLine();

    imprimirDibujo(numeroVides); //Mostra el dibuix del penjat segons el nombre d'intents que li queden al jugador creat en la funció.

    if (numeroEncerts == paraulaAEncertar.Length) break; //Si el nombre d'encerts ja es igual al nombre de lletres es surt del WHILE.
    if (numeroVides == 0) break; //Si el nombre de vides es 0 es surt del WHILE.

    lletraEscollida = pedirLetra(); //El jugador pot escriure una lletra tal com s'ha creat en la funció.

    for (int i = 0; i < 3; i++)     //Bucle el qual recorre les 27 lletres del abecedari i crida la funció per veure si la lletra esta repetida, si es el cas torna
    {
        for (int j = 0; j < 9; j++)
        {
            int startcheck = 0;
            Lletrarepetida(abecedari[i, j], lletraEscollida, checker[i, j], ref startcheck);
            if (startcheck == 1) goto start;
        }
    }

    
    int EncertsCheck = numeroEncerts; //Int que serveix per a guardar temporalment com a referencia el nombre d'encerts abans d'actualitzar-los.
    int checkcount = 0;
    for (int i = 0; i < paraulaAEncertar.Length; i++) //Busca si hi ha un nou encert, lletra a lletra tal com s'ha creat en la funció, si no conta només un error.
    {
        actualizarAciertos(lletraEscollida, paraulaAEncertar[i], ref paraulaACompletar[i], ref numeroEncerts, EncertsCheck, ref numeroVides, paraulaAEncertar.Length, ref checkcount);
    }
}

Console.WriteLine();
Console.WriteLine();
Console.WriteLine("*********************************************************");
Console.WriteLine("*                                                       *");

//Si el nombre d'encerts de les lletres es igual a la llargada en lletres de la paraula, es mostra la pantalla de victoria.
if (numeroEncerts == paraulaAEncertar.Length)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\t       Enhorabona, has guanyat!");
    Console.ResetColor();
}
//En cas contrari es mostra la pantalla de derrota amb la paraula que no s'ha endevinat.
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\tHas perdut, la paraula correcta era: " + paraulaAEncertar);
    Console.ResetColor();
}

Console.WriteLine("*                                                       *");
Console.WriteLine("*********************************************************");
Console.ReadKey();

Console.WriteLine();
//Es pregunta si es vol tornar a jugar.
Console.WriteLine("\t      Vol tornar a jugar? SI o NO");
Jugar: //Punt de referencia en al codi per a tornar a poder preguntar el valor en cas de no ser ni si ni no
Console.Write("\t      ");
string data = Console.ReadLine();
switch (data)
{
    case "SI": //Si es diu si, el joc es reinicia.
        goto restart;

    case "NO": //Si es diu no, el joc finalitza.
        break;

    default: //Si no es diu ni si ni no, es torna a fer la pregunta i es torna al punt de referencia anterior.
        goto Jugar;
}
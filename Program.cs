using System;
using Python.Runtime;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace projnet
{
    public class YourClass
    {
        public static void Main(string[] args)
        {
            Runtime.PythonDLL = @"C:\Users\LENOVO\AppData\Local\Programs\Python\Python310\python310.dll";
            PythonEngine.Initialize();

            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append(@"C:\Users\LENOVO\Desktop\backendv0"); // répertoire contenant le script Python

                dynamic module = Py.Import("open"); // nom du module Python sans l'extension .py
                module.extract_text_from_image(@"C:/Users/LENOVO/Desktop/backendv0/CTN471.jpg", @"C:/Users/LENOVO/Desktop/backendv0"); // appeler la fonction avec les bons arguments
            }
            
            // Lire le contenu du fichier texte
            string filePath = @"C:\Users\LENOVO\Desktop\backendv2\CTN471.txt";
            string content = File.ReadAllText(filePath);
            Console.WriteLine(content);


            // Extraire les données à partir du fichier texte
            string factureText = content;
            string numero = ExtractValue(@"M S� acture (\d+)", content);
            string date = ExtractValue(@": Sa (\w+) Le (\d{2}/\d{2}/\d{4}) \d{2}:\d{2}", content, 2);
            string ville = ExtractValue(@": Sa (\w+) Le (\d{2}/\d{2}/\d{4}) \d{2}:\d{2}", content, 1);
            string codeClient = ExtractValue(@"Code Client : (\d+)", content);
            string codeFiscal = ExtractValue(@"Code Fiscal : (\w+)", content);
            string navire = ExtractValue(@"Navire : (\w+) Du : (\d{2}/\d{2}/\d{4})", content, 1);
            string blNumero = ExtractValue(@"B/L Numero : (\d+) Dossier no : (\w+)", content, 1);
            string blDate = ExtractValue(@"Navire : (\w+) Du : (\d{2}/\d{2}/\d{4})", content, 2);
            string provenance = ExtractValue(@"Provenance : (\w+) Destination : (\w+)", content, 1);
            string destination = ExtractValue(@"Provenance : (\w+) Destination : (\w+)", content, 2);
            string dossierNo = ExtractValue(@"B/L Numero : (\d+) Dossier no : (\w+)", content, 2);

             // Chaîne de connexion à la base de données
                string connectionString = "Data Source=DESKTOP-UHV52H6;Initial Catalog=CTN;Integrated Security=True";

                // Créer une nouvelle connexion à la base de données
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Ouvrir la connexion à la base de données
                    connection.Open();

                // Insérer les données dans la table Marque
                string marque1 = ExtractValue(@"ER\d+ ([\d,]+) \| (.+?) ([\d,]+|[\d]+) (\w+)", content.Replace("�", " "), 2);
                float poids1 = float.Parse(ExtractValue(@"ER\d+ ([\d,]+) \| (.+?) ([\d,]+|[\d]+) (\w+)", content.Replace("�", " "), 1).Replace(',', '.'));
                string marque2 = ExtractValue(@"45 ([\d,]+) \| (.+?) ([\d,]+|[\d]+) (\w+)", content.Replace("�", " "), 2);
                float poids2 = float.Parse(ExtractValue(@"45 ([\d,]+) \| (.+?) ([\d,]+|[\d]+) (\w+)", content.Replace("�", " "), 1).Replace(',', '.'));

                string sqlMarque = "INSERT INTO Marque (marque, col_mtg1, libelleMarchandise1, poids1, col_mtg2, libelleMarchandise2, poids2) VALUES (@marque, @col_mtg1, @libelleMarchandise1, @poids1, @col_mtg2, @libelleMarchandise2, @poids2)";
                using (SqlCommand command = new SqlCommand(sqlMarque, connection))
                {
                    command.Parameters.AddWithValue("@marque", "CTN471");
                    command.Parameters.AddWithValue("@col_mtg1", "ER9422C");
                    command.Parameters.AddWithValue("@libelleMarchandise1", marque1);
                    command.Parameters.AddWithValue("@poids1", poids1);
                    command.Parameters.AddWithValue("@col_mtg2", "45");
                    command.Parameters.AddWithValue("@libelleMarchandise2", marque2);
                    command.Parameters.AddWithValue("@poids2", poids2);
                    command.ExecuteNonQuery();
                }

// Insérer les données dans la table CodeFiscal
string sqlCodeFiscal = "INSERT INTO CodeFiscal (id, navire, provenance, blNumero, blDate, destination, dossierNo) VALUES (@id, @navire, @provenance, @blNumero, @blDate, @destination, @dossierNo)";
using (SqlCommand command = new SqlCommand(sqlCodeFiscal, connection))
{
command.Parameters.AddWithValue("@id", Int32.Parse(codeFiscal));
command.Parameters.AddWithValue("@navire", navire);
command.Parameters.AddWithValue("@provenance", provenance);
command.Parameters.AddWithValue("@blNumero", blNumero);
command.Parameters.AddWithValue("@blDate", DateTime.Parse(blDate));
command.Parameters.AddWithValue("@destination", destination);
command.Parameters.AddWithValue("@dossierNo", dossierNo);
command.ExecuteNonQuery();
}

// Insérer les données dans la table Facture
float avanceEuro = float.Parse(ExtractValue(@"Totaux en EUR Totaux en millime\nAvance : (\d+,\d+) Avance : (\d+)\nDestination : (\d+,\d+) Destination : (\d+)", content, 1).Replace(',', '.'));
int avanceMillimes = int.Parse(ExtractValue(@"Totaux en EUR Totaux en millime\nAvance : (\d+,\d+) Avance : (\d+)\nDestination : (\d+,\d+) Destination : (\d+)", content, 2));
float destinationEuro = float.Parse(ExtractValue(@"Totaux en EUR Totaux en millime\nAvance : (\d+,\d+) Avance : (\d+)\nDestination : (\d+,\d+) Destination : (\d+)", content, 3).Replace(',', '.'));
int destinationMillimes = int.Parse(ExtractValue(@"Totaux en EUR Totaux en millime\nAvance : (\d+,\d+) Avance : (\d+)\nDestination : (\d+,\d+) Destination : (\d+)", content, 4));
string sqlFacture = "INSERT INTO Facture (numero, date, ville, pays, nomFacture, codeClient, codeFiscal, avanceEuro, destinationEuro, avanceMillimes, destinationMillimes, marque_id) VALUES (@numero, @date, @ville, @pays, @nomFacture, @codeClient, @codeFiscal, @avanceEuro, @destinationEuro, @avanceMillimes, @destinationMillimes, @marque_id)";
using (SqlCommand command = new SqlCommand(sqlFacture, connection))
{
command.Parameters.AddWithValue("@numero", numero);
command.Parameters.AddWithValue("@date", DateTime.Parse(date));
command.Parameters.AddWithValue("@ville", ville);
command.Parameters.AddWithValue("@pays", "Tunisie");
command.Parameters.AddWithValue("@nomFacture", "Facture d'achat");
command.Parameters.AddWithValue("@codeClient", codeClient);
command.Parameters.AddWithValue("@codeFiscal", Int32.Parse(codeFiscal));
command.Parameters.AddWithValue("@avanceEuro", avanceEuro);
command.Parameters.AddWithValue("@destinationEuro", destinationEuro);
command.Parameters.AddWithValue("@avanceMillimes", avanceMillimes);
command.Parameters.AddWithValue("@destinationMillimes", destinationMillimes);
command.Parameters.AddWithValue("@marque_id", "CTN471");
command.ExecuteNonQuery();
}

// Insérer les données dans la table Rubrique
string sqlRubrique = "INSERT INTO Rubrique (num, rubrique, base, taux, montant, a_d, facture_numero) VALUES (@num, @rubrique, @base, @taux, @montant, @a_d, @facture_numero)";
using (SqlCommand command = new SqlCommand(sqlRubrique, connection))
{
// Extraire les données des rubriques
string rubriquesText = ExtractValue(@"DO EC AS AE M\n(.+?)\nTotaux en EUR Totaux en millime", content, 1);
string[] rubriqueLines = rubriquesText.Split('\n');
foreach (string rubriqueLine in rubriqueLines)
{
if (string.IsNullOrWhiteSpace(rubriqueLine))
{
continue;
}
string[] rubriqueValues = rubriqueLine.Split(' ');
string num = rubriqueValues[0];
string rubrique = rubriqueValues[1];
float baseMontant = float.Parse(rubriqueValues[2].Replace(',', '.'));
float taux = float.Parse(rubriqueValues[3].Replace(',', '.'));
float montant = float.Parse(rubriqueValues[4].Replace(',', '.'));
string aD = rubriqueValues[5];

command.Parameters.Clear();
command.Parameters.AddWithValue("@num", num);
command.Parameters.AddWithValue("@rubrique", rubrique);
command.Parameters.AddWithValue("@base", baseMontant);
command.Parameters.AddWithValue("@taux", taux);
command.Parameters.AddWithValue("@montant", montant);
command.Parameters.AddWithValue("@a_d", aD);
command.Parameters.AddWithValue("@facture_numero", numero);
command.ExecuteNonQuery();
}
}
}
}

// Fonction pour extraire une valeur à partir d'une expression régulière
public static string ExtractValue(string pattern, string input, int groupIndex = 0)
{
    Match match = Regex.Match(input, pattern);
    if (match.Success)
    {
        return match.Groups[groupIndex].Value;
    }
    else
    {
        throw new Exception($"Aucune correspondance trouvée pour le motif '{pattern}' dans le texte.");
    }
}

}
}


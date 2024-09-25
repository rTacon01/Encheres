using Encheres.Modeles;
using Encheres.Services;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        /// <summary>
        /// Test de la connexion d'un utilisateur � l'API en utilisant un mot de passe et un email pr�sent dans la Base de donn�es
        /// </summary>
        [Test]
        public void ConnexionUtilisateurSuccess()
        {
            // Jeu d'essai
            ApiAuthentification _apiServicesAuthentification = new ApiAuthentification();
            string _email = "romain@gmail.com";
            string _password = "romain";
            bool _resultat = false;


            //Gr�ce � l'adresse email et le mot de passe entr�s en valeur, on demande � l'API de v�rifier
            //l'exactitude des informations. Dans ce cas, l'API ne devrait rien retourner car les informations sont fausses.
            var task = _apiServicesAuthentification.GetAuthAsync<User>
                   (_email, _password, "api/getUserByMailAndPass");
            
            // On r�cup�re le r�sultat de la requ�te et on l'instancie � un utilisateur.
            User res = task.Result;

            //Afin de savoir si un utilisateur a bien �t� renvoy�e par l'api ou non, on cr�er une condition qui va nous dire si 
            // la variable res contient des �l�ments ou non.
            if (res != null)
            {
                _resultat = true;
            }

            //Assert : Le r�sultat attendu est true car les informations envoy�es � la BDD correspondent � un utilisateur 
            Assert.AreEqual(true, _resultat);
        }





        /// <summary>
        /// Test de la connexion d'un utilisateur � l'API en utilisant un mot de passe et un email qui ne correspondent pas � un utilisateur inscrit dans la BDD
        /// </summary>
        [Test]
        public void ConnexionUtilisateurFailure()
        {
            // Jeu d'essai
            ApiAuthentification _apiServicesAuthentification = new ApiAuthentification();
            string _password = "rom";
            string _email = "romaintest@test.com";
            bool _resultat = false;


            //Gr�ce � l'adresse email et le mot de passe entr�s en valeur, on demande � l'API de v�rifier
            //l'exactitude des informations, et va renvoyer les informations de l'utilisateur si l'utilisateur est bien pr�sent dans la BDD.
            var task = _apiServicesAuthentification.GetAuthAsync<User>
                   (_email, _password, "api/getUserByMailAndPass");

            // On r�cup�re le r�sultat de la requ�te et on l'instancie � un utilisateur.
            User res = task.Result;

            // Afin de savoir si un utilisateur a bien �t� renvoy�e par l'api ou non, on cr�er une condition qui va nous dire si 
            // la variable res contient des �l�ments ou non.
            if (res != null)
            {
                _resultat = true;
            }

            //Assert : Le r�sultat attendu est false car les informations envoy�es � la BDD ne correspondent � aucun utilisateur pr�sent dans la BDD
            Assert.AreEqual(false, _resultat);
        }

    }
}
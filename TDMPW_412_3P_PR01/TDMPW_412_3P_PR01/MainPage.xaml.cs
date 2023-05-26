using System.ComponentModel;



namespace TDMPW_412_3P_PR01;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    //   Score: 5   
    int[] indice = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };
    int contador = 0;
    int puntaje = 0;
    int win = 3;
    int oportunidadesPorPregunta = 2;
    int contadorOportunidadesPorPregunta = 0; 
    private string message = "OK!";
    int mistakes = 0;
    int maxWrong = 3;
    private string gameStatus = "   Score: 0 ";
    private string question = "";
    private string currentImage = "";
    private string answer = "";
    List<char> guessed = new List<char>();

    public string CurrentImage
    {
        get => currentImage;
        set
        {
            currentImage = value;
            OnPropertyChanged();
        }
    } 

    public string Preguntas
    {
        get => question;
        set
        {
            question = value;
            OnPropertyChanged();
        }
    }

    public string Message
    {
        get => message;
        set
        {
            message = value;
            OnPropertyChanged();
        }
    }

    public string GameStatus
    {
        get => gameStatus;
        set
        {
            gameStatus = value;
            OnPropertyChanged();
        }
    }



    List<string> preguntas = new List<string>()
    {
        "¿Quién es el máximo goleador de la historia del fútbol?",
        "¿Qué selección ha ganado más Copas Mundiales de Fútbol?",
        "¿Dónde se celebraron los Juegos Olímpicos del año 1992?",
        "¿En qué país se encuentra el estadio de Wembley?",
        "¿Cómo se llama el estadio del F.C. Barcelona?",
        "¿Cuántas veces ha ganado España un Mundial?",
        "¿Quién es el atleta con más medallas olímpicas?",
        "¿Quién es el futbolista con más balones de oro?",
        "¿Cuántos anillos hay en la bandera olímpica?",
        "¿Cuál es el deporte más practicado del mundo?"
    };

    List<string> respuestas = new List<string>()
    {
        "Cristiano Ronaldo",
        "Brasil",
        "Barcelona",
        "Reino Unido",
        "Camp Nou",
        "Una",
        "Michael Phelps",
        "Messi",
        "Cinco",
        "Natación"
    };

    List<string> imagenes = new List<string>()
    {
        "puno.jpg",
        "pdos.jpeg",
        "ptres.jpg",
        "pcuatro.jpg",
        "pcinco.jpg",
        "pseis.png",
        "psiete.jpg",
        "pocho.jpg",
        "pnueve.jpg",
        "pdiez.png"
    };

    public MainPage()
    {
        InitializeComponent();
        Shuffle(indice);
        BindingContext = this;
        Message = "OK!";
        contador = 0;
        setearValoresAleatorios();
    }

    private void setearValoresAleatorios()
    {
        Preguntas = preguntas[indice[contador]];
        CurrentImage = imagenes[indice[contador]];
    }

    private void veredicto() {
        if (puntaje == win && contador <= 4)
        {
            CurrentImage = "ganar.jpg";
            this.boton.BackgroundColor = Color.FromRgb(251, 163, 26);
            Message = "Reset";
            Preguntas = "¡Felicidades, ganaste!";
            this.respuesta.Text = "";
            this.respuesta.BackgroundColor = Color.FromRgb(255, 255, 255);

        }
        else {
            if (contador > 4)
            {
                this.boton.BackgroundColor = Color.FromRgb(251, 163, 26);
                CurrentImage = "perder.jpg";
                Message = "Reset";
                Preguntas = "¡Lo siento, perdiste!";
                this.respuesta.Text = "";
                this.respuesta.BackgroundColor = Color.FromRgb(255, 255, 255);
            }
        }
    }

    private void chequeoRespuesta() {
        if (this.respuesta.Text == respuestas[indice[contador]])
        {
            contador++;
            puntaje++;
            GameStatus = $"   Score: {puntaje}   ";
            contadorOportunidadesPorPregunta = 0;
            this.respuesta.BackgroundColor = Color.FromRgb(0, 150, 0);
            this.respuesta.TextColor = Color.FromRgb(255, 255, 255);
            setearValoresAleatorios();
            
        }
        else {
            this.respuesta.BackgroundColor = Color.FromRgb(255, 0, 0);
            this.respuesta.TextColor = Color.FromRgb(255, 255, 255);
            Preguntas = preguntas[indice[contador]];
            CurrentImage = imagenes[indice[contador]];
            contadorOportunidadesPorPregunta++;
            if (this.respuesta.Text == respuestas[indice[contador]])
            {
                contador++;
                puntaje++;
                GameStatus = $"   Score: {puntaje}   ";
                contadorOportunidadesPorPregunta = 0;
                this.respuesta.BackgroundColor = Color.FromRgb(0, 150, 0);
                this.respuesta.TextColor = Color.FromRgb(255, 255, 255);
                setearValoresAleatorios();
            }
            else {


                if (contadorOportunidadesPorPregunta == oportunidadesPorPregunta)
                {
                    contadorOportunidadesPorPregunta = 0;
                    contador++;
                    this.respuesta.BackgroundColor = Color.FromRgb(255, 0, 0);
                    this.respuesta.TextColor = Color.FromRgb(255, 255, 255);
                    setearValoresAleatorios();
                }
            }
            
        }
        veredicto();
    }
    
    
    void boton_Clicked(System.Object sender, System.EventArgs e)
    {
        if (Message == "Reset")
        {
            this.respuesta.Text = "";
            this.respuesta.BackgroundColor = Color.FromRgb(255, 255, 255);
            puntaje = 0;
            contador = 0;
            Shuffle(indice);
            Message = "OK!";
            GameStatus = "   Score: 0 ";
            contador = 0;
            this.boton.BackgroundColor = Color.FromRgb(0, 128, 0);
            this.respuesta.TextColor = Color.FromRgb(0,0,0);
            setearValoresAleatorios();
        }
        else {
            chequeoRespuesta();
            this.respuesta.Text = "";
        }
    }
    
    public static void Shuffle<T>(T[] arreglo)
    {
        Random rnd = new Random();
        int n = arreglo.Length;
        while (n > 1)
        {
            int k = rnd.Next(n--);
            T temp = arreglo[n];
            arreglo[n] = arreglo[k];
            arreglo[k] = temp;
        }
    }
}



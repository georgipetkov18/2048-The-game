using Game2048.DataAccess.Repositories;
using Game2048.ViewModels;
using System.Collections.ObjectModel;

namespace Game2048;

public partial class ScoreboardPage : ContentPage
{
    private readonly ScoreRepository scoreRepository;

    public ObservableCollection<ScoreboardPageViewModel> Scores { get; set; } = [];

    public ScoreboardPage(ScoreRepository scoreRepository)
	{
		InitializeComponent();
        this.scoreRepository = scoreRepository;
        this.BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        this.Scores.Clear();

        var scores = await this.scoreRepository.GetScoresAsync();

        foreach (var score in scores)
        {
            this.Scores.Add(new ScoreboardPageViewModel(score.Points, score.CreatedOn.ToString("dd.MM.yyyy HH:mm"), score.Moves));
        }
    }
}
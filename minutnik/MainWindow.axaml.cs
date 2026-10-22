using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;

namespace minutnik;

public partial class MainWindow : Window
{
    private enum TimerPhase
    {
        Work,
        Break
    }

    private readonly DispatcherTimer _timer;
    private TimerPhase _currentPhase = TimerPhase.Work;
    private bool _isRunning = false;
    private int _remainingSeconds = 25 * 60;
    private int _completedCycles = 0;

    public MainWindow()
    {
        InitializeComponent();

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += Timer_Tick;

        StartPauseButton.Click += StartPauseButton_Click;
        ResetButton.Click += ResetButton_Click;

        WorkDurationNumeric.ValueChanged += WorkDurationNumeric_ValueChanged;
        BreakDurationNumeric.ValueChanged += BreakDurationNumeric_ValueChanged;

        InitializeTimerFromSettings();
        UpdateAllDisplays();
    }

    private void InitializeTimerFromSettings()
    {
        int workMinutes = Math.Max(1, (int)(WorkDurationNumeric.Value ?? 25));
        _remainingSeconds = workMinutes * 60;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (_remainingSeconds > 0)
        {
            _remainingSeconds--;
            UpdateTimerDisplay();
        }

        if (_remainingSeconds <= 0)
        {
            HandlePhaseTransition();
        }
    }

    private void HandlePhaseTransition()
    {
        if (_currentPhase == TimerPhase.Work)
        {
            // Work finished: switch to break and start break automatically
            _currentPhase = TimerPhase.Break;
            int breakMinutes = Math.Max(1, (int)(BreakDurationNumeric.Value ?? 5));
            _remainingSeconds = breakMinutes * 60;

            UpdatePhaseDisplay();
            UpdateTimerDisplay();
            StatusTextBlock.Text = "Czas pracy minął! Przerwa wystartowała automatycznie.";

            TryPlayNotificationBeep();
        }
        else
        {
            // Break finished: full cycle finished!
            _completedCycles++;
            UpdateCycleDisplay();

            _currentPhase = TimerPhase.Work;
            int workMinutes = Math.Max(1, (int)(WorkDurationNumeric.Value ?? 25));
            _remainingSeconds = workMinutes * 60;

            UpdatePhaseDisplay();
            UpdateTimerDisplay();
            StatusTextBlock.Text = $"Przerwa zakończona! Cykl ukończony. Rozpoczynanie cyklu {_completedCycles + 1}.";

            TryPlayNotificationBeep();
        }
    }

    private void StartPauseButton_Click(object? sender, RoutedEventArgs e)
    {
        if (!_isRunning)
        {
            _isRunning = true;
            _timer.Start();

            StartPauseButton.Content = "Pauza";
            StartPauseButton.Background = Brush.Parse("#E11D48");

            StatusTextBlock.Text = _currentPhase == TimerPhase.Work
                ? "Odmierzanie czasu pracy..."
                : "Odmierzanie czasu przerwy...";
        }
        else
        {
            _isRunning = false;
            _timer.Stop();

            StartPauseButton.Content = "Start";
            StartPauseButton.Background = Brush.Parse("#4F46E5");

            StatusTextBlock.Text = "Minutnik wstrzymany (pauza).";
        }
    }

    private void ResetButton_Click(object? sender, RoutedEventArgs e)
    {
        _timer.Stop();
        _isRunning = false;
        _currentPhase = TimerPhase.Work;
        _completedCycles = 0;

        int workMinutes = Math.Max(1, (int)(WorkDurationNumeric.Value ?? 25));
        _remainingSeconds = workMinutes * 60;

        StartPauseButton.Content = "Start";
        StartPauseButton.Background = Brush.Parse("#4F46E5");

        StatusTextBlock.Text = "Zresetowano minutnik i licznik cykli.";

        UpdateAllDisplays();
    }

    private void WorkDurationNumeric_ValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        if (!_isRunning && _currentPhase == TimerPhase.Work)
        {
            int minutes = (int)(e.NewValue ?? 25);
            _remainingSeconds = Math.Max(1, minutes) * 60;
            UpdateTimerDisplay();
        }
    }

    private void BreakDurationNumeric_ValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        if (!_isRunning && _currentPhase == TimerPhase.Break)
        {
            int minutes = (int)(e.NewValue ?? 5);
            _remainingSeconds = Math.Max(1, minutes) * 60;
            UpdateTimerDisplay();
        }
    }

    private void UpdateAllDisplays()
    {
        UpdatePhaseDisplay();
        UpdateTimerDisplay();
        UpdateCycleDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = _remainingSeconds / 60;
        int seconds = _remainingSeconds % 60;
        TimerTextBlock.Text = $"{minutes:D2}:{seconds:D2}";
    }

    private void UpdateCycleDisplay()
    {
        CycleCounterTextBlock.Text = _completedCycles.ToString();
    }

    private void UpdatePhaseDisplay()
    {
        if (_currentPhase == TimerPhase.Work)
        {
            PhaseTextBlock.Text = "💼 CZAS PRACY";
            PhaseTextBlock.Foreground = Brush.Parse("#A5B4FC");
            PhaseBadgeBorder.Background = Brush.Parse("#1E1B4B");
            PhaseBadgeBorder.BorderBrush = Brush.Parse("#6366F1");
        }
        else
        {
            PhaseTextBlock.Text = "☕ CZAS PRZERWY";
            PhaseTextBlock.Foreground = Brush.Parse("#6EE7B7");
            PhaseBadgeBorder.Background = Brush.Parse("#064E3B");
            PhaseBadgeBorder.BorderBrush = Brush.Parse("#10B981");
        }
    }

    private static void TryPlayNotificationBeep()
    {
        try
        {
            Console.Beep();
        }
        catch
        {
            // System might not support console beep in GUI mode
        }
    }
}
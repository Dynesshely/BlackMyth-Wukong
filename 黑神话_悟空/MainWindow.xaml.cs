using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Timer = System.Timers.Timer;

namespace 黑神话_悟空
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                Thread.Sleep(App.Random.Next(App.Random.Next(1500, 2500), App.Random.Next(4500, 7500)));
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    WindowStyle = WindowStyle.None;
                    WindowState = WindowState.Maximized;
                    Cursor = Cursors.Arrow;
                }));

                Thread.Sleep(App.Random.Next(App.Random.Next(500, 1000), App.Random.Next(1500, 2000)));
                Begin_EntranceAnimation();
            }).Start();

            InitCustomComponents();
        }

        private void InitCustomComponents()
        {
            LogoPresenter.Opacity = 0;
            BackgroundPresenter.Opacity = 0;

            IconPresenter.Source = new BitmapImage(new Uri($"{App.WorkBase}\\src\\steam\\img_logo_a.png"));
        }

        private void Begin_EntranceAnimation()
        {
            string logo_dir = "\\src\\logo\\";
            int logo_index = 0;

            Timer timer = new()
            {
                Interval = 3500,
                AutoReset = true
            };
            timer.Elapsed += (x, y) =>
            {
                switch (logo_index)
                {
                    case 0:
                        TransformLogo($"{App.WorkBase}{logo_dir}game_science_logo.png");
                        break;
                    case 1:
                        TransformLogo($"{App.WorkBase}{logo_dir}epic_logo.png");
                        break;
                    case 2:
                        TransformLogo($"{App.WorkBase}{logo_dir}unreal_logo.png");
                        break;
                    case 3:
                        TransformLogo($"{App.WorkBase}{logo_dir}nvidia_geforce_logo.png");
                        break;
                    default:
                        BeginBackgroundMusicLoop();
                        BeginBackgroundVideoLoop();
                        PresentMainMenuUI();
                        CanButtonClick = true;
                        timer.Stop();
                        timer.Dispose();
                        break;
                }
                ++logo_index;
            };
            timer.Start();
        }

        private void TransformLogo(string logo_Path)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                LogoPresenter.Source = new BitmapImage(new(logo_Path));
                LogoPresenter.BeginAnimation(OpacityProperty, new DoubleAnimation()
                {
                    Duration = new(new(0, 0, 0, 0, 800)),
                    From = 0,
                    To = 1,
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction = new CubicEase()
                    {
                        EasingMode = EasingMode.EaseIn
                    }
                });
                new Thread(() =>
                {
                    Thread.Sleep(1500);

                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        LogoPresenter.BeginAnimation(OpacityProperty, new DoubleAnimation()
                        {
                            Duration = new(new(0, 0, 0, 0, 800)),
                            From = 1,
                            To = 0,
                            FillBehavior = FillBehavior.HoldEnd,
                            EasingFunction = new CubicEase()
                            {
                                EasingMode = EasingMode.EaseIn
                            }
                        });
                    }));
                }).Start();
            }));
        }

        private static void BeginBackgroundMusicLoop()
        {
            App.SoundPlayer.SoundLocation = $"{App.WorkBase}\\src\\music\\main_menu_bgmsc.wav";
            App.SoundPlayer.PlayLooping();
        }

        private void BeginBackgroundVideoLoop()
        {
            TransformBgVedio(1);
        }

        private bool ContinueTransform = true;
        private bool CanButtonClick = false;

        private void TransformBgVedio(int index)
        {
            string video_dir = "\\src\\video\\";
            string video_Path = $"{App.WorkBase}{video_dir}video_Day{index}.mp4";

            Dispatcher.BeginInvoke(new Action(() =>
            {
                BackgroundPresenter.MediaEnded += (x, y) =>
                {
                    if (ContinueTransform)
                    {
                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            BackgroundPresenter.BeginAnimation(OpacityProperty, new DoubleAnimation()
                            {
                                Duration = new(new(0, 0, 0, 0, 800)),
                                From = 1,
                                To = 0,
                                FillBehavior = FillBehavior.HoldEnd,
                                EasingFunction = new CubicEase()
                                {
                                    EasingMode = EasingMode.EaseIn
                                }
                            });
                        }));
                        TransformBgVedio(index == 6 ? 1 : index + 1);
                    }
                };
                BackgroundPresenter.Source = new Uri(video_Path);
                BackgroundPresenter.BeginAnimation(OpacityProperty, new DoubleAnimation()
                {
                    Duration = new(new(0, 0, 0, 0, 800)),
                    From = 0,
                    To = 1,
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction = new CubicEase()
                    {
                        EasingMode = EasingMode.EaseIn
                    }
                });
            }));
        }

        private void PresentMainMenuUI(bool isFadeOut = false)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                UIContainer.BeginAnimation(OpacityProperty, new DoubleAnimation()
                {
                    Duration = new(new(0, 0, 0, 0, 800)),
                    From = isFadeOut ? 1 : 0,
                    To = isFadeOut ? 0 : 1,
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction = new CubicEase()
                    {
                        EasingMode = EasingMode.EaseIn
                    }
                });
            }));
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            if (CanButtonClick)
                Environment.Exit(0);
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            if (CanButtonClick)
            {
                PresentMainMenuUI(true);
                App.SoundPlayer.Stop();
                CanButtonClick = false;
                Cursor = Cursors.None;

                ContinueTransform = false;

                BackgroundPresenter.MediaEnded += (x, y) =>
                {
                    Cursor = Cursors.Arrow;

                    new Thread(() =>
                    {
                        Thread.Sleep(App.Random.Next(2500, 3000));
                        Process.Start($"{App.WorkBase}\\崩溃提示.exe");
                        Environment.Exit(1);
                    }).Start();

                    MessageBox.Show("error: 0x114514", "fatal", MessageBoxButton.OK, MessageBoxImage.Error);
                };

                new Thread(() =>
                {
                    Thread.Sleep(800);

                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        UIContainer.Opacity = 0;
                        BackgroundPresenter.Source = new Uri($"{App.WorkBase}\\src\\video\\黑悟空内测流出.24fps.mp4");
                        BackgroundPresenter.IsMuted = false;
                        BackgroundPresenter.Opacity = 1;
                    }));
                }).Start();

            }
        }
    }
}

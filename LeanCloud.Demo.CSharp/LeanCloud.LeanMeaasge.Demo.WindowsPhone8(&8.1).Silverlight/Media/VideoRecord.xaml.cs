using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

// Directives
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Shell;
using System.Windows.Navigation;
using Microsoft.Devices;
using Windows.Phone.Media.Capture;
using LeanCloud.LeanMeaasge.Demo.Resources;
using Windows.Storage.Streams;
using Windows.Storage;
using Microsoft.Phone.Tasks;

namespace LeanCloud.LeanMeaasge.Demo.Media
{
    public partial class VideoRecord : PhoneApplicationPage
    {
        PhoneApplicationService myService = PhoneApplicationService.Current;
    
        private IsolatedStorageFileStream isoVideoFile;
        private FileSink fileSink;
        private string recordVideoFileName = "record_video_temp.mp4";
        private AudioVideoCaptureDevice _captureDevice;
        private IRandomAccessStream _stream;
     
        // Constructor
        public VideoRecord()
        {
            InitializeComponent();
            this.Loaded += VideoRecord_Loaded;
        }
        private async void VideoRecord_Loaded(object sender, RoutedEventArgs e)
        {
            // 一些概述类的说明
            Summary();

            // 是否有后置摄像头
            if (AudioVideoCaptureDevice.AvailableSensorLocations.Contains(CameraSensorLocation.Back))
            {
                // 获取后置摄像头摄像时的可用分辨率
                IReadOnlyList<Windows.Foundation.Size> supportedResolutions = AudioVideoCaptureDevice.GetAvailableCaptureResolutions(CameraSensorLocation.Back);
                Windows.Foundation.Size resolution = supportedResolutions[0];

                try
                {
                    // 让后置摄像头以指定的分辨率捕获镜头内容
                    _captureDevice = await AudioVideoCaptureDevice.OpenAsync(CameraSensorLocation.Back, resolution);
                    // AudioVideoCaptureDevice.OpenForVideoOnlyAsync() - 仅捕获视频
                    // AudioVideoCaptureDevice.OpenForAudioOnlyAsync() - 仅捕获音频

                    // 录像失败时触发的事件
                    _captureDevice.RecordingFailed += _captureDevice_RecordingFailed;


                    /*
                     * SetCaptureResolutionAsync() - 设置摄像的分辨率
                     * CaptureResolution - 获取当前摄像的分辨率
                     * VideoEncodingFormat - 当前的视频编码格式
                     * AudioEncodingFormat - 当前的音频编码格式
                     * FocusRegion - 对焦区域
                     * SensorLocation - 当前摄像头的位置（CameraSensorLocation 枚举：Back 或 Front）
                     * SensorRotationInDegrees - 获取摄像头传感器相对于屏幕的旋转度数
                     * FocusAsync() - 自动对焦
                     * ResetFocusAsync() - 复位对焦
                     */


                    /*
                     * KnownCameraAudioVideoProperties 属性集包括
                     *     VideoFrameRate - 每秒抓取的视频帧数
                     *     H264EncodingProfile - H264 编码的 profile（H264EncoderProfile 枚举）
                     *     H264EncodingLevel - H264 编码的 level（H264EncoderLevel 枚举）
                     *     H264EnableKeyframes - 是否启用关键帧
                     *     H264QuantizationParameter - QP 值，低的 QP 会保留大部分空间的详细信息，从而达到最佳质量，高的 QP 会在一定程度上造成质量的损失，但能帮助编码器实现较低的比特率
                     *     H264RequestDropNextNFrames - 指定编码器应丢弃的帧数
                     *     H264RequestIdrFrame - 此属性设置为 true 时，系统请求编码流程进行瞬时解码刷新(IDR)
                     *     UnmuteAudioWhileRecording - 此属性设置为 true 时，能在记录期间为音频取消静音
                     *     VideoTorchMode - 录像时如何使用闪光灯（VideoTorchMode 枚举：Off, Auto, On）
                     *     VideoTorchPower - 录像时闪光灯的亮度，无单位且不同设备上的值不同
                     */
                    _captureDevice.SetProperty(KnownCameraAudioVideoProperties.H264EncodingProfile, H264EncoderProfile.Baseline);


                    /*
                     * KnownCameraGeneralProperties 属性集包括
                     *     AutoFocusRange - 自动对焦的范围（AutoFocusRange 枚举，包括微距等）
                     *     EncodeWithOrientation - 视频编码时的旋转角度，必须是 90 的倍数
                     *     SpecifiedCaptureOrientation -  元数据中的旋转角度，必须是 90 的倍数
                     *     IsShutterSoundEnabledByUser - 用户是否启用了快门声音，只读
                     *     IsShutterSoundRequiredForRegion - 运行应用程序的区域是否需要快门声音（有些区域为了保护隐私，要求照相或录像必须要有快门声音），只读
                     *     PlayShutterSoundOnCapture - 指定捕获时是否播放快门声音
                     *     ManualFocusPosition - 手动对焦的位置
                     */
                    _captureDevice.SetProperty(KnownCameraGeneralProperties.AutoFocusRange, AutoFocusRange.Normal);
                    _captureDevice.SetProperty(KnownCameraGeneralProperties.EncodeWithOrientation, _captureDevice.SensorRotationInDegrees);


                    // 获取指定属性的值
                    // _captureDevice.GetProperty(KnownCameraGeneralProperties.IsShutterSoundEnabledByUser);

                    /*
                     * 获取指定的范围类属性在当前摄像头中所允许的值的范围
                     */
                    // AudioVideoCaptureDevice.GetSupportedPropertyRange(CameraSensorLocation.Back, KnownCameraAudioVideoProperties.H264QuantizationParameter);

                    /*
                     * 获取指定的值类属性在当前摄像头中所允许的值的列表
                     */
                    // AudioVideoCaptureDevice.GetSupportedPropertyValues(CameraSensorLocation.Back, KnownCameraAudioVideoProperties.H264EncodingProfile);

                    // 实时显示捕获的内容
                    videoBrush.SetSource(_captureDevice); // 扩展方法来自：Microsoft.Devices.CameraVideoBrushExtensions

                    rt.Angle = _captureDevice.SensorRotationInDegrees;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("没有后置摄像头");
            }
        }

        // 录像失败
        void _captureDevice_RecordingFailed(AudioVideoCaptureDevice sender, CaptureFailedEventArgs args)
        {
            this.Dispatcher.BeginInvoke(delegate()
            {
                MessageBox.Show("error: " + args.ErrorCode.ToString());
            });
        }

        // 开始录像
        private async void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 获取应用程序数据存储文件夹
                StorageFolder applicationFolder = ApplicationData.Current.LocalFolder;

                // 在指定的应用程序数据存储文件夹内创建指定的文件
                StorageFile storageFile = await applicationFolder.CreateFileAsync(recordVideoFileName, CreationCollisionOption.ReplaceExisting);

                // 打开文件流，准备写入录像数据
                _stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite);

                // 录制视频到指定的流
                await _captureDevice.StartRecordingToStreamAsync(_stream);

                btnCapture.IsEnabled = false;
                btnStop.IsEnabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // 停止录像
        private async void btnStop_Click(object sender, RoutedEventArgs e)
        {
            // 停止录像
            await _captureDevice.StopRecordingAsync();
            _stream.Dispose();

            btnCapture.IsEnabled = true;
            btnStop.IsEnabled = false;
        }

        // 播放录制的内容
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            // 启动媒体播放器，播放录制的内容
            MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();

            // new Uri("webabcdTest.mp4", UriKind.Relative) 结合 MediaLocationType.Data，则系统会先在应用程序存储的 Local 目录下找，找不到再到 Local/IsolatedStorage 目录下找
            mediaPlayerLauncher.Media = new Uri(recordVideoFileName, UriKind.Relative);
            mediaPlayerLauncher.Location = MediaLocationType.Data;
            mediaPlayerLauncher.Controls = MediaPlaybackControls.Pause | MediaPlaybackControls.Stop;
            mediaPlayerLauncher.Orientation = MediaPlayerOrientation.Landscape;

            mediaPlayerLauncher.Show();
        }


        private void Summary()
        {
            lblMsg.Text = "";

            // 获取电话上的可用摄像头
            foreach (CameraSensorLocation csl in AudioVideoCaptureDevice.AvailableSensorLocations)
            {
                // Back 或 Front
                lblMsg.Text += "摄像头：" + csl.ToString();
                lblMsg.Text += System.Environment.NewLine;


                // 摄像所支持的分辨率
                lblMsg.Text += "摄像的可用分辨率：";
                foreach (var size in AudioVideoCaptureDevice.GetAvailableCaptureResolutions(csl))
                {
                    lblMsg.Text += size.Width + "*" + size.Height + " ";
                }
                lblMsg.Text += System.Environment.NewLine;
                lblMsg.Text += System.Environment.NewLine;
            }


            lblMsg.Text += "终端所支持的视频编码格式：";
            foreach (CameraCaptureVideoFormat format in AudioVideoCaptureDevice.SupportedVideoEncodingFormats)
            {
                lblMsg.Text += format.ToString() + " ";
            }
            lblMsg.Text += System.Environment.NewLine;


            lblMsg.Text += "终端所支持的音频编码格式：";
            foreach (CameraCaptureAudioFormat format in AudioVideoCaptureDevice.SupportedAudioEncodingFormats)
            {
                lblMsg.Text += format.ToString() + " ";
            }
            lblMsg.Text += System.Environment.NewLine;
        }
      
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Dispose of camera and media objects.
            base.OnNavigatedFrom(e);
        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            myService.State["action"] = "Send_Video";
            NavigationService.GoBack();
        }

       

    }
}
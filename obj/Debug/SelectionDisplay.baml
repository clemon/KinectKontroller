   M S B A M L   `   `   `  ����  ControlsBasics-WPF>    8Microsoft.Samples.Kinect.ControlsBasics.SelectionDisplay   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      
������ $��Segoe UI)�5
      $��30(�5        � 	Resources  5         OnLoadedStoryboard �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value R� KeyTime  System.Windows.Visibility   Hidden R� Value��%#  &	        �� 5   
   -   5        .��5      U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�6,   $
 grid��6�   .\�5        $ 0��6'   $	 0.1Q�63     5      $ 0:0:0.2��6'   $ 1Q�69     5      $ 0:0:2.2��6'   $ 1Q�69     5      $ 0:0:2.4��6'   $	 0.1Q�69   5      U� 5      $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�6,   $
 grid��6�   .\�5        $ 0��6'   $	 0.1Q�63     5      $ 0:0:0.2��6'   $ 1Q�69     5      $ 0:0:2.2��6'   $ 1Q�69     5      $ 0:0:2.4��6'   $	 0.1Q�69   5      T� 5      $ (UIElement.Visibility)�6,   $
 grid��6_   ..�5      i� $ 0:0:2.4��6)   # Z 6;   5      5      5       � Triggers 5      :� 5    
   	 :� RoutedEvent$	 FrameworkElement.Loaded��6   .[�5!      �� -   5        
 ��Name$(
 "OnLoadedStoryboard_BeginStoryboard��5!      #��[  6J   5"      .��5$      � -   5         �Name$ 
layoutRoot��5$      .+�5%   
   � -   5        $
 grid��5%        	BlueBrush#��[ 6    J� RenderTransformOrigin$ 0.5,0.5&�6E   z�5&      V� 5'      .~�5(      �� �� 5)      �� 5*      T� 5+      5,      .+�5.      �� -   5         ��Name$ messageTextBlock��5.      
�������$��Center=�6E   $��Center=�6b   You selected item #146�   5/      50      51      
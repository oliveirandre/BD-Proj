   M S B A M L   `   `   `  ����I  EProjetoFinalBD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ProjetoFinalBDProjetoFinalBD  5   	       ProjetoFinalBD.HomeWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ProjetoFinalBD     5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   $	��600��5      $
��1080��6'     � 
ResizeMode$  NoResize=�64    � WindowStartupLocation$ CenterScreen=�6J   
������� � 	Resources 5	         MenuOpen �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  	MenuClose��%�  &	        &	 �     �� 5
   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridMenu��6a   .\�5        $ 0��6'   $ 60Q�63     5      $ 0:0:0.5��6'   $	 150Q�69   5      5      �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridMenu��6a   .\�5        $ 0��6'   $	 150Q�63     5      $ 0:0:0.5��6'   $ 60Q�69   5      5      5       � Triggers 5      :� 5   
    :� RoutedEvent$ ButtonBase.Click��6   	 :� 
SourceName$	 ButtonOpenMenu��66   .[�5      �� #��[  6   5      :� 5   
   $ ButtonBase.Click��6   $	 ButtonCloseMenu��66   .[�5      �� #��[ 6   5      .��5!      � 
�������.+�5"   
   � $��60��6   $	��Top=�6   ���	#FF26569B.+�5#      �� $��Sample Keeper��6   $��Center=�6-   $��Center=�6J   $��22(�6e   6u   �� 5$      $��Center=�6   
 �� Orientation$
 
Horizontal=�64   $��Right=�6M   .+�5%      �� -   5         ��Name$ username��5%      $��Center=�6.   $��18(�6I   '  !MaterialDesignThemes.Wpf.PopupBox  5&      $��10q�6*      PlacementMode$ BottomAndAlignRightEdges=�66      	StaysOpen .     PopupContent. 5'      �� $	��150��6!   .+�5(      �� $��Account��6!   �� 5)      $��Settings��6!   �� 5*      6&   �� 5+      -   5         ��Name$ BottonPopUpLogout��5+   !   $��Logout��6<   6M   5,      5-      5.      5/      � 51   
   $��60 60 0 260q�6   
�������53      � 54   
   $��
60 315 0 0q�6   56      � 58   
   -   5         �Name$ GridMenu��58      $��60��6!   $
��Left=�6,   ���	#FF211874.+�59      �� .+�5:      � $��60��6   
�������.+�5;      �� -   5        $ ButtonCloseMenu��5;      $��60��66   $��60��6A   $	��Top=�6{   $��Right=�6�   $y�	Collapsed=�6�   ��6M   Z� ��6c   Z� .��5<      '  !MaterialDesignThemes.Wpf.PackIcon  ���	#FF211874�  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ 	ArrowLeft=�6I   $��25��6Z   $��25��6e   5=      �� 5>      -   5        $ ButtonOpenMenu��5>      $��60��65   $��60��6@   $	��Top=�6z   $��Right=�6�   ��6L   Z� ��6b   Z� .��5?        ���	#FF211874$
 Menu=�6I   $��25��6U   $��25��6`   5@      5A      �� 5B      $��Disabled=�6   ���	#FF26569B.@�5C      �� -   5        $��60��5C   #   .��5D      �� $
 
Horizontal=�6%   .+�5E        $
 Home=�66   $��25��6B   $��25��6M   $��10q�6Y   $��Center=�6e   6�   �� 5F      $
��Home��6(   $��Center=�64   $��10 10q�6O   6`   5G      5H      �� 5I      -   5        $��60��5I   #   .��5J      �� $
 
Horizontal=�6%   .+�5K        $ 	SearchWeb=�66   $��25��6G   $��25��6R   $��10q�6^   $��Center=�6j   6�   �� 5L      $��Search��6(   $��Center=�66   $��10 10q�6Q   6b   5M      5N      �� 5O      -   5        $��60��5O   #   .��5P      �� $
 
Horizontal=�6%   .+�5Q        $ PlaylistPlay=�66   $��25��6J   $��25��6U   $��10q�6a   $��Center=�6m   6�   �� 5R      $��	Playlists��6(   $��Center=�69   $��10 10q�6T   6e   5S      5T      �� 5U      -	   5        $��60��5U   #   .��5V      �� $
 
Horizontal=�6%   .+�5W        $ 	GithubBox=�66   $��25��6G   $��25��6R   $��10q�6^   $��Center=�6j   6�   �� 5X      $��GitHub��6(   $��Center=�66   $��10 10q�6Q   5Y      5Z      5[      5\      5]      5^      5_      5`      
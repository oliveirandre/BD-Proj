   M S B A M L   `   `   `  ����I  EProjetoFinalBD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ProjetoFinalBDProjetoFinalBD  5   	       ProjetoFinalBD.NewAlbum   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ProjetoFinalBD     5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   $	��450��5      $	��500��6'     � 
ResizeMode$  NoResize=�63    � WindowStartupLocation$ CenterScreen=�6I   
�������.��5	      � $��20q�6   .+�5
   
   �� $	��159��6   $	��Top=�6!   $��
0,0,-0.4,0q�69   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FF26569B��6#   67   5      5      �� 5   
   $	��296��6   
�������$��Bottom=�6.   $��62,0,56.6,38q�6I    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ 	#FFBBBBBB��6#      	Direction$ 0Q�65      RenderingBias$ Quality=�6C   	   
BlurRadius$	 15Q�6[   
   ShadowDepth$
 1Q�6k   6}   5      �� 5   
   -   5         ��Name$ albumnumber��5       �� 	MaxLength$ 15��6'   $��Center=�66   ���	#FF211874)  #MaterialDesignThemes.Wpf.HintAssist   Hint$$ CATALOG NUMBER (15 CHARACTERS)��6j    %  !MaterialDesignFloatingHintTextBox#��[  6�   $	��Top=�6�   $
��0 60q�6�   $	��300��6  6  �� 5   
   -   5        $ album��5      $��Center=�6!   ���	#FF211874$ 
ALBUM NAME��6U   #��[  6�   $	��Top=�6�   $��0 100q�6�   $	��300��6�   6�   �� 5   
   -   5        $
 year��5      $��Center=�6    ���	#FF211874$ YEAR OF RELEASE��6T   #��[  6�   $	��Top=�6�   $��0 140q�6�   $	��300��6�   6�   �� 5   
   -   5        $ label��5      $��Center=�6!   ���	#FF211874$ 
LABEL NAME��6U   #��[  6�   $��Bottom=�6�   $��0 160q�6�   $	��300��6�   6�   �� 5   
   -   5        $ artist��5      $��Center=�6"   $��Bottom=�6?   ���	#FF211874$ ARTIST NAME��6q   #��[  6�   $��0 120q�6�   $	��300��6�   6�   �� 5   
   -   5        $ image��5      $��Center=�6!   $��Bottom=�6>   ���	#FF211874$ IMAGE (ex: album.png)��6p   #��[  6�   $
��0 80q�6�   $	��300��6�   6�   �� 5   
   -   5        $	��100��5      $��Bottom=�6   $��Confirm��68   $��177,0,176.6,23.8q�6J   6{   �� 5   
   -   5         ��Name$ hidden��5      $��Center=�6    $��Bottom=�6=   
���  ��6k   5      5      
   M S B A M L   `   `   `  ����I  EProjetoFinalBD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ProjetoFinalBDProjetoFinalBD  5   	   !    ProjetoFinalBD.SignupWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ProjetoFinalBD     5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   $	��500��5      $	��350��6'     � 
ResizeMode$  NoResize=�63    � WindowStartupLocation$ CenterScreen=�6I   
�������.��5	      � .+�5
   
   �� $	��280��6   $	��Top=�6!   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FF26569B��6#   67   5      5      �� 5   
   $	��280��6   $	��350��6    
�������$��Center=�6:    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ 	#FFBBBBBB��6#      	Direction$ 0Q�65      RenderingBias$ Quality=�6C   	   
BlurRadius$	 15Q�6[   
   ShadowDepth$
 1Q�6k   6}   5      � 5   
   $	��280��6   $	��350��6   $��Center=�6(   .+�5      �� $��SIGN UP��6   $��Center=�6'   $	��Top=�6D   $��5q�6\   
�������$��18(�6y   6�   �� 5      $��Center=�6   $��10q�64   .+�5      �� -   5         ��Name$ txtUsername��5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ USERNAME��6/    %  !MaterialDesignFloatingHintTextBox#��[  6Y   $
��0 10q�6�   
�������$��18(�6�   6�   �� 5      -   5        $ txtEmail��5      $ EMAIL��6,   #��[  6S   $
��0 10q�6�   
�������$��18(�6�   6�   G� 5      -   5         G�Name$ txtPassword��5      $ PASSWORD��63    ) %MaterialDesignFloatingHintPasswordBox#��[ 6]   $
��0 10q�6�   
�������$��18(�6�   6�   G� 5      -   5        $ txtPassword2��5      $ REPEAT PASSWORD��64   #��[ 6e   $
��0 10q�6�   
�������$��18(�6�   6�   5      5      �� 5   
   -   5        $��Bottom=�5      $
��0,40q�6,   $	��150��6:   $��SIGN UP��6F   6u   �� 5    
   -   5         ��Name$ label1��5       $�� ��6    $��Bottom=�6+   $��Center=�6F   $��10q�6c   
���  ��$y�Visible=�6�   6�   5!      5"      
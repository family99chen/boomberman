   M S B A M L   `   `   `  ����D  @Bombermen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Bombermen	Bombermen  5   	       Bombermen.Main   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Bombermen     5   	   -   5          � Title$
  Main��5   	   $	��450��6   $	��800��6#    � WindowStartupLocation$ CenterScreen=�6D   .��5	      � $��
0,0,0,-130q�6   .+�5
   
   �� -   5         ��Name$ 	game_canv��5
      $
��0,50q�6$   �� 5   
   -   5         ��Name$
 tbur��5      $
��Left=�6"   $��273,518,0,0q�6=   $
��Wrap=�6R   $��	TextBlock��6f   $	��Top=�6w   $y�Hidden=�6�   �� 5   
   -   5        $ tbkey��5      $
��Left=�6#   $��130,515,0,0q�6>   $
��Wrap=�6S   $��	TextBlock��6g   $	��Top=�6x   $y�Hidden=�6�   �� 5   
   -   5        $
 tbun��5      $
��Left=�6"   $��385,518,0,0q�6=   $
��Wrap=�6R   $��	TextBlock��6f   $	��Top=�6w   $y�Hidden=�6�   �� 5   
   -   5        $ score��5      $
��Left=�6#   $��497,518,0,0q�6>   $
��Wrap=�6S   $��	TextBlock��6g   $	��Top=�6x   $y�Hidden=�6�   �� 5   
   $
��Left=�6   $��130,535,0,0q�6-    �� TextWrapping$
 Wrap=�6B   $��钥匙位置��6V   $	��Top=�6b   $	��120��6z   $y�Hidden=�6�   �� 5   
   $
��Left=�6   $��273,538,0,0q�6-   $
 Wrap=�6B   $��升级-range位置��6V   $	��Top=�6h   $	��120��6�   $y�Hidden=�6�   �� 5   
   $
��Left=�6   $��377,538,0,0q�6-   $
 Wrap=�6B   $��升级-炸弹数量位置��6V   $	��Top=�6g   $	��120��6   $y�Hidden=�6�   �� 5   
   $
��Left=�6   $��497,535,0,0q�6-   $
 Wrap=�6B   $��1,1sym��6V   $	��Top=�6d   $	��120��6|   $y�Hidden=�6�   �� 5   
   -   5        $ tbdoor��5      $
��Left=�6$   $��
36,513,0,0q�6?   $
��Wrap=�6S   $��	TextBlock��6g   $	��Top=�6x   $y�Hidden=�6�   �� 5   
   $
��Left=�6   $��
10,538,0,0q�6-   $
 Wrap=�6A   $��	门位置��6U   $	��Top=�6`   $	��120��6x   $y�Hidden=�6�   �� 5   
   -   5         ��Name$
 time��5      $
��Left=�6    $��
95,533,0,0q�6;   $
 Wrap=�6O   $��Time:��6c   $	��Top=�6p   $	��190��6�   
��� � �
����  �$��0q�6�   
�������$��20��6�   $��16(�6�   $y�Hidden=�6�   �� 5   
   -	   5         ��Name$ restart��5      $��restart��6"   $
��Left=�64   $��162,516,0,0q�6O   $	��Top=�6d   $��29��6|   $��56��6�   $y�Hidden=�6�   �� 5   
   -
   5        $
 menu��5      $
��menu��6   $
��Left=�6.   $��398,516,0,0q�6I   $	��Top=�6^   $��29��6v   $��55��6�   $y�Hidden=�6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      5      5      
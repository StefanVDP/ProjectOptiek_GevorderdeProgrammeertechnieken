   M S B A M L   `   `   `  ����L  HOptiekProject_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:OptiekProject_WPFOptiekProject_WPF  5   	   *    $OptiekProject_WPF.OverzichtSportbril   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:OptiekProject_WPF     5   	   -   5          �Name$  ProductOverzichtWindow��5   	    � Title$ 	Overzicht��5	   	   $	��450��6   $	��800��6(   .��5
      � .+�5   
   �� .+�5      �� $��1q�6   
���   �._�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $
��Home��6   $��30��6,   $	��130��68   $��50,5,5,5q�6D   $
��Home��6j   ��6V   �� 6�   �� 5      $��	Sportbril��6   $��30��61   $	��130��6=   $��5q�6I   $��	Sportbril��6h   ��6T   �� 6�   �� 5      $��Leesbril��6   $��30��60   $	��130��6<   $��5q�6H   $��Leesbril��6g   ��6S   �� 6�   �� 5      $��
Schietbril��6   $��30��62   $	��130��6>   $��5q�6J   $��
Schietbril��6i   ��6U   �� 6�   �� 5      $��	Zonnebril��6   $��30��61   $	��130��6=   $��5q�6I   $��	Zonnebril��6h   ��6T   �� 6�   5      5      �� 5      $ 
Horizontal=�6   .+�5      �� $
��Left=�6   $	��380��68   $	��190��6E   .+�5      �� $��30��6   ��6*   �� *
Textfilter+�� 5      $��30��6    �� DisplayMemberPath$
 Naam��6O    �� SelectedItem 6+   �� *
Merkfilter+��6h   �� *Merken+5      � 5      $	��380��6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dgdOverzicht��5      $	��550��6V      AutoGenerateColumns .     
IsReadOnly . $��20q�6�   ��65   �� *
Modellen+ 6�   �� *SelectedModel+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Naam��6J   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 61   �� *Naam+6J     5      $	 Prijs Model��6K   
 61   �� *Prijs+6K     5      $	 Omschrijving��6R   
 61   �� *Omschrijving+6R   5!      5"      5#      5%      5&      5'      
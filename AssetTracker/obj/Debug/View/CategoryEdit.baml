   M S B A M L   `   `   `  ����G  CAssetTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AssetTracker.ViewAssetTracker.View  5      A%clr-namespace:AssetTracker.ViewModelsAssetTracker.ViewModels  5      H DMaterialIcons, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:MaterialIcons;assembly=MaterialIconsMaterialIcons 5      $    AssetTracker.View.CategoryEdit   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:AssetTracker.View     5      ;	viewmodel%clr-namespace:AssetTracker.ViewModels     5      Ami2clr-namespace:MaterialIcons;assembly=MaterialIcons     5        N� Title$  CategoryEdit��5      .��5      � $��100,0q�6    � 	Resources 5   
    �� 
TargetType �� Property   	Text_Dark �� Value��%V   '��       7   �� 5      " ��6   .�5      ��  � ��8   5      5      5       � ColumnDefinitions 5   
   �� 5      $��2* �6   $	��300��5      �� 5      $��5* �6   .+�5   
   �� -   5         ��Name$ 	LeftPanel��5        	LeftPanel#��[ 5      ._�5      �� .+�5       �� -   5        $��ControlButtons��5         ControlBtns#��[ 5!      ._�5#      ��  �� Orientation$ 
Horizontal=�6"   $��Center=�5$   "   .+�5&      �� $��0,0,30,0q�5(   !     
ControlBtn#��[ 63   {�5&   !   �� *	IsSavable+��5'   !   �� *SaveCommand+.��5)         MaterialIcons.MaterialIcon     Icon$ ic_save=�6.     HoverBtn_Content#��[ 5*   .   5+      �� 5-      #��[ 6!   	 � Triggers	 5.      :� 5/   "   
 :� RoutedEvent$
 Button.Click��6/   .[�50   &   �� .��51   *   �� .��52   .   T�  �� 
TargetName$ DeletePrompt��6L    �� TargetProperty$ 
Visibility�53   L   ..�54   2   i�  R� KeyTime$ 0:0:0��6I     System.Windows.Visibility   Visible R� Value# Z 55   I   56   /   57   +   58   '   59   #   .��5;        $ 	ic_delete=�6.   #��[ 5=   .   5>      5@      5A      �� 5E        LeftPanel_Header#��[ 5F      $
��0,10q�5G      ��5E      �� *HeadingContent+5G      �� 5H      $ 
Horizontal=�6   .+�5I      �� $��0,0,10,0q�5J       ��5I       �� *Category.ca_name+5J       �� 5K      
�������$��Center=�5L       $��Right=�5M       �� 5N      $�#��6    �� 5O      �6   �� *Category.ca_id+5P      5S      5T      5U      �� 5V   
   -   5        $ 
RightPanel��5V      $��1��5W        
RightPanel#��[ 5X      ._�5Z      � .+�5[      �� $ Vertical=�6   $��50q�5\      .+�5^      �� $��Setup information��6      RightPanel_Header#��[ 5_       $
��0,20q�5`       �� 5b      $��Category Name��6    �� 5c      $ 
Horizontal=�6!   $��0,0,0,20q�5d   !   .+�5e      �� $	��275��5f      $
��Left=�5g      $��16(�5h        Layer3_Dark#��[ 5i      #��[  5j      ��5e   "   �� *CategoryName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5j      �� 5k       		 Error#��[	 6)   $��Center=�5l   "   $��5,0,0,0q�5m   "   ��5k   R   �� *CategoryNameViolation+y�5n   '   �� *CategoryNameViolation+ 
 IsNullInvertToVizConverter �� 	Converter# [
 5o      �� 5q      $ 
Horizontal=�6!   .+�5r      �� -   5        $��+ Add Phase��5r   !   $	��275��5t      $
��Left=�5u      $��0q�5v      #��[ 5w      #��[  5x      $��SemiBold"�5y      �� 5{      #��[	 6)   $��5,0,0,0q�6R   ��6c   �� *PhasesViolation+y�5|   )   �� *PhasesViolation+# [
 5}      �� 5      -   5         ��Name$ 	PhaseList��5   #   
������ $��0q�5�   #   $
��Left=�5�   #   ��5�   #   �� *CurrentPhases+��5�      �� 5�      .R�5�   "   �� 
������� �� Padding$ 5q�5�   )   $	��275��5�   )   ._�5�   &   �� $ 
Horizontal=�61    5�   *   ��%�   '��       '  N     �� 5�   .   " ��64   .�5�   2   ��  � ��
 ���� 5�   9   5�   /   �� 5�   .   "  64   .�5�   2   ��  � ��$ 20��5�   9   �� 5�   2    � ��$ 20��5�   9   5�   /   5�   +   .+�5�   *   �� $��Center=�5�   4   ��5�   4   �� *	ph_step+5�   4   �� 5�   *   -   5        $
��10,0q�5�   2   $	��150��5�   2   $��Center=�5�   2   ��5�   2   �� *Name+5�   2   �� 5�   *   -   5        .��5�   .     $ ic_arrow_upward=�6>   5�   +   �� 5�   *   -   5        $	��5,0q�5�   1   .��5�   .     $ ic_arrow_downward=�6>   5�   +   �� 5�   *   -	   5        .��5�   .     $ 	ic_delete=�6>   5�   +   5�   '   5�   #   5�      5�      5�      �� 5�      -
   5         ��Name$ PromptSavePanel��5�        
PromptSave#��[ 5�      y�5�      �� *
PromptSave+   BooleanToVisibilityConverter# [ �� 5�      -   5        $ DeletePrompt��5�      $y�	Collapsed=�5�        Overlay#��[ 5�      $��Center=�5�      $��Center=�5�      $ 30q�5�      ._�5�      �� .+�5�      �� $ 
Horizontal=�6%   .+�5�        $ ic_delete_forever=�6.     TextOverlay_Dark#��[ 5�   9   �� 5�      $��Delete Asset��6(   $��Center=�5�   (   5�      �� 5�      $��Center=�6$   $
��0,50q�5�   $   $��16(�5�   $   �� 5�      $%�Are you sure you want to delete��6    �� 5�      $
��Bold"�5�      �5�      �� *Category.Name+5�       �� 5�      $�#��6   $
��Bold"�5�       �� 5�      $
��Bold"�5�      �5�      �� *Category.ID+5�       �� 5�      $�?��6   5�      �� 5�      $ 
Horizontal=�6%   $��Center=�5�   %   .+�5�      �� $��	0,10,20,0q�6%     HoverBtn#��[ 5�   %   $��5q�5�   %   $��5q�5�   %     ConfirmOverlay_Dark#��[ 5�   %   $	��150��5�   %   ��5�   %   �� *DeleteConfirmed+	 5�   "   :� 5�   &   $
 Button.Click��63   .[�5�   *   �� .��5�   .   �� .��5�   2   T� $ DeletePrompt��6P   $ 
Visibility�5�   P   ..�5�   6   i� $ 0:0:0��6M      	Collapsed# Z 5�   M   5�   3   5�   /   5�   +   5�   '   .��5�   "   �� $��Confirm��6,     HoverBtn_TextContent#��[ 5�   ,   $��Center=�5�   ,   $��Center=�5�   ,   #��[ 5�   ,   $��SemiBold"�5�   ,   $��14(�5�   ,   5�      �� 5�      $��	0,10,20,0q�6&   $	��150��5�   &   #��[ 5�   &   $��5q�5�   &   $��5q�5�   &     RejectOverlay_Dark#��[ 5�   &   	 5�   "   :� 5�   &   $
 Button.Click��63   .[�5�   *   �� .��5�   .   �� .��5�   2   T� $ DeletePrompt��6P   $ 
Visibility�5�   P   ..�5�   6   i� $ 0:0:0��6M   # Z 5�   M   5�   3   5�   /   5�   +   5�   '   .��5  "   �� $��Cancel��6,   #��[ 5  ,   $��Center=�5  ,   $��Center=�5  ,   #��[ 5  ,   $��SemiBold"�5  ,   $��14(�5  ,   5     5
     5     5     5     5     5     5     
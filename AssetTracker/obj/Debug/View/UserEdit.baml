   M S B A M L   `   `   `  ����G  CAssetTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AssetTracker.ViewAssetTracker.View  5      A%clr-namespace:AssetTracker.ViewModelsAssetTracker.ViewModels  5      H DMaterialIcons, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:MaterialIcons;assembly=MaterialIconsMaterialIcons 5      K*clr-namespace:AssetTracker.View.ConvertersAssetTracker.View.Converters  5	      G(clr-namespace:AssetTracker.View.ControlsAssetTracker.View.Controls  5
           AssetTracker.View.UserEdit   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:AssetTracker.View     5      ;	viewmodel%clr-namespace:AssetTracker.ViewModels     5      Ami2clr-namespace:MaterialIcons;assembly=MaterialIcons     5      A
converters*clr-namespace:AssetTracker.View.Converters     5	      =controls(clr-namespace:AssetTracker.View.Controls     5
        N� Title$  UserEdit��5      .��5      � $��100,0q�6    � ColumnDefinitions 5   
   �� 5      $��2* �6   $	��300��5      �� 5      $��5* �6   .+�5   
   �� -   5         ��Name$ 	LeftPanel��5         	LeftPanel#��[  5      ._�5      � .+�5      �� .+�5      �� -   5        $��ControlButtons��5        ControlBtns#��[ 5      ._�5      ��  �� Orientation$ 
Horizontal=�6&   $��Center=�5   &   .+�5       �� $��0,0,30,0q�5"   %     
ControlBtn#��[ 5#   %   {�5    %   �� *	IsSavable+��5!   %   �� *SaveCommand+.��5$   "      MaterialIcons.MaterialIcon     Icon$ ic_save=�62     HoverBtn_Content#��[ 5%   2   5&      �� 5(      #��[ 5)   %   ��5(   %   �� *DeleteConfirmed+ � Triggers 5*   "   :� 5+   &    :� RoutedEvent$ Button.Click��63   .[�5,   *   �� .��5-   .   �� .��5.   2   T�  �� 
TargetName$ DeletePrompt��6P    �� TargetProperty$ 
Visibility�5/   P   ..�50   6   i� 	 R� KeyTime$	 0:0:0��6M     System.Windows.Visibility
   Visible R� Value# Z
 51   M   52   3   53   /   54   +   55   '   .��57   "     $ 	ic_delete=�62   #��[ 58   2   59      5;      5<      �� 5>      -   5         ��Name$ HeadingText��5>   !     	Text_Dark#��[ 5@   !   $��24(�5A   !   $
��Bold"�5B   !   $
��0,10q�5C   !   ��5?   !   �� *HeadingContext+5C   !   �� 5E      $ 
Horizontal=�6!   .+�5F      �� $��0,0,10,0q�5G   $   #��[ 5H   $   ��5F   $   �� *CurrentUser.Name+5H   $   �� 5I      
�������y�5J   $   �� *
Creating+  InvertBoolToViz �� 	Converter# [ �� 5K      $�#��6    �� 5L      �6   �� *CurrentUser.ID+5M      5N      5O      �� 5P      $��20,250q�6   .+�5Q      2   ,AssetTracker.View.Controls.SubNavRadioButton  -   5          Name$ UserSetupNav��5Q   2      LabelContent$ 
User Setup��5R   "    	� 	GroupName$ SubNav��5S   "      
IconChoice$ 	ic_person=�5T   "    d� 	IsChecked$
 TrueY�5U   "   $��0,0,0,20q�63     5V      -   5        $ PermissionsNav��5V   2   $ Permissions��5W   "   $ SubNav��5X   "   $ ic_verified_user=�5Y   "   5Z      5[      5\      �� 5^   
   -   5        $ 
RightPanel��5^      $��1��5_        
RightPanel#��[ 5`       � 	Resources 5a       �� 
TargetType �� Property �� Value��%V   '��       7  �� 5b      " ��6   .�5c      ��  � ��8   5d      5e      5f      ._�5g      � .+�5h      �� $��50q�6   .+�5j      � -   5         �Name$ 	UserSetup��5j      $
��0,10q�5k      y�5l      ��  �� ElementName$ UserSetupNav�� �� Path$ 	IsChecked�   BooleanToVisibilityConverter# [  5m        Layer3_Dark��%!  '��       7  7  �� 5n      " ��6$   .�5o   "   ��  � ��$	 200��5p   )   �� 5q   "    � ��$	 0,5q�5r   )   �� 5s   "    � ��$
 Left=�5t   )   �� 5u   "    � ��8   5v   )   �� 5w   "    � ��8  5x   )   �� 5y   "    � ��$ 1q�5z   )   5{      5}      .+�5      �� $��Setup information��6$   $��16(�5�   $   $��SemiBold"�5�   $   � 5�      $
��0,80q�6$    � Orientation$ Vertical=�5�   $   .+�5�      �� $��
First Name��6(   �� 5�      $ 
Horizontal=�6)   .+�5�   "   �� ��6*   �� *	FirstName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5�   "    		 Error#��[	 61   $��5,0,0,0q�5�   1   ��5�   1   �� *FirstNameViolation+y�5�   1   �� *FirstNameViolation+ 
 IsNullInvertToVizConverter# [
 5�      �� 5�      $��	Last Name��6(   $��0,10,0,0q�5�   (   �� 5�      $ 
Horizontal=�6)   .+�5�   "   �� ��6*   �� *
LastName+$ PropertyChanged=��� 5�   "   #��[	 61   $��5,0,0,0q�5�   1   ��5�   1   �� *LastNameViolation+y�5�   1   �� *LastNameViolation+# [
 5�      �� 5�      $��Display Name��6(   $��0,10,0,0q�5�   (   �� 5�      $ 
Horizontal=�6)   .+�5�   "   �� ��6*   �� *DisplayName+$ PropertyChanged=��� 5�   "   #��[	 61   $��5,0,0,0q�5�   1   ��5�   1   �� *DisplayNameViolation+y�5�   1   �� *DisplayNameViolation+# [
 5�      �� 5�      $��Email��6(   $��0,10,0,0q�5�   (   �� 5�      $ 
Horizontal=�6)   .+�5�   "   �� ��6*   �� *Email+$ PropertyChanged=��� 5�   "   #��[	 61   $��5,0,0,0q�5�   1   ��5�   1   �� *EmailViolation+y�5�   1   �� *EmailViolation+# [
 5�      �� 5�      $��Password��6(   $��0,10,0,0q�5�   (   �� 5�      $ 
Horizontal=�6)   .+�5�   "   �� ��6*   �� *
Password+$ PropertyChanged=��� 5�   "   #��[	 61   $��5,0,0,0q�5�   1   ��5�   1   �� *PasswordViolation+y�5�   1   �� *PasswordViolation+# [
 5�      �� 5�      $
��Role��6(   $��0,10,0,0q�5�   (   �� 5�      $ 
Horizontal=�6)   .+�5�   "   *   $AssetTracker.View.Controls.Searchbox  -   5          Name$ Searchbox_Role��5�   5   $	��300��5�   2   $��30��5�   2      Items 5�   2   �� *Roles+   CurrentSelection 5�   2   �� *
UserRole+$ TwoWay=��� 5�   "   #��[	 61   $��5,0,0,0q�5�   1   ��5�   1   �� *RoleUnassignedViolation+y�5�   1   �� *RoleUnassignedViolation+# [
 5�      5�      5�      �� 5�      -	   5          ��Name$  Permissions��5�   !   $
��0,10q�5�   !   y�5�   !   �� $ PermissionsNav��$ 	IsChecked�# [ .+�5�      �� $��Modify Permissions��6$   $��16(�5�   $   $��SemiBold"�5�   $   $��0,0,0,50q�6:   �� 5�      $ 
Horizontal=�6%   .+�5�      ��   HoverBtn#��[ 6%   $��	0,0,15,10q�5�   %   ��5�   %   �� *ResetAllPermissionsCommand+.��5�   "   �� $ Vertical=�6-   .+�5�   &     $ 
ic_refresh=�66     ListHeaderBtn_Image#��[ 5�   6   �� 5�   &   $��Reset��60     ListHeaderBtn_Text#��[ 5�   0   5�   #   5�      �� 5�      #��[ 6%   $��	0,0,15,10q�5�   %   ��5�   %   �� *ActivateAllPermissionsCommand+.��5�   "   �� $ Vertical=�6-   .+�5�   &     $ ic_radio_button_checked=�66   #��[ 5�   6   �� 5�   &   $��Activate All��60   #��[ 5�   0   5�   #   5�      �� 5�      #��[ 6%   $��	0,0,15,10q�5�   %   ��5�   %   �� *!DeactivateAllPermissionsCommand+.��5�   "   �� $ Vertical=�6-   .+�5�   &     $ ic_radio_button_unchecked=�66   #��[ 5�   6   �� 5�   &   $��Deactivate All��60   #��[ 5�   0   5�   #   5�      5�      �� 5�        PermissionsList#��[ 5�   '   ��5�   '   �� *PermissionGroups+5�   '   5�      5�      �� 5�      -
   5        ! ��Name$! PromptSavePanel��5�        
PromptSave#��[ 5�      y�5�      �� *
PromptSave+# [ �� 5      -   5        $ DeletePrompt��5      $y�	Collapsed=�5       Overlay#��[ 5     $��Center=�5     $��Center=�5     " �� Padding$" 30q�5     ._�5     �� .+�5     �� $ 
Horizontal=�6%   .+�5       $ ic_delete_forever=�6.     TextOverlay_Dark#��[ 5	  .   �� 5
     $��Delete Asset��6(   $��Center=�5  (   5     �� 5     $��Center=�6$   $
��0,50q�5  $   $��16(�5  $   �� 5     $%�Are you sure you want to delete��6    �� 5     $
��Bold"�5     �5     �� *CurrentUser.Name+5      �� 5     $�#��6   $
��Bold"�5      �� 5     $
��Bold"�5     �5     �� *CurrentUser.ID+5      �� 5     $�?��6   5     �� 5     $ 
Horizontal=�6%   $��Center=�5  %   .+�5     �� $��	0,10,20,0q�6%   #��[ 5  %   $��5q�5  %   $��5q�5   %     ConfirmOverlay_Dark#��[ 5!  %   $	��150��5#  %   ��5"  %   �� *DeleteConfirmed+.��5$  "   �� $��Confirm��6,     HoverBtn_TextContent#��[ 5%  ,   $��Center=�5&  ,   $��Center=�5'  ,   #��[ 5(  ,   $��SemiBold"�5)  ,   $��14(�5*  ,   5+     �� 5,     $��	0,10,20,0q�6&   $	��150��5-  &   #��[ 5.  &   $��5q�5/  &   $��5q�50  &     RejectOverlay_Dark#��[ 51  &    52  "   :� 53  &   $ Button.Click��63   .[�54  *   �� .��55  .   �� .��56  2   T� $ DeletePrompt��6P   $ 
Visibility�57  P   ..�58  6   i� $	 0:0:0��6M   #   	Collapsed# Z# 59  M   5:  3   5;  /   5<  +   5=  '   .��5?  "   �� $��Cancel��6,   #��[ 5@  ,   $��Center=�5A  ,   $��Center=�5B  ,   #��[ 5C  ,   $��SemiBold"�5D  ,   $��14(�5E  ,   5F     5H     5J     5K     5L     5M     5N     5O     
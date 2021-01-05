import 'package:flutter/material.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/cupertino.dart';

import 'login.dart';

void main() {
  runApp(ChatApp());
}

final ThemeData _defaultTheme = buildDefaultLightTheme();

ThemeData buildDefaultLightTheme() {
  final ThemeData base = ThemeData.light();
  return base.copyWith(
      primaryColor: Colors.purple,
      accentColor: Colors.orangeAccent[400],
      buttonTheme: ButtonThemeData(
          disabledColor: Colors.grey,
          buttonColor: Colors.purple,
          hoverColor: Colors.purpleAccent));
}

class ChatApp extends StatelessWidget {
  const ChatApp({
    Key key,
  }) : super(key: key);

  final String title = 'Y.A.P.C';

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: title,
      theme: _defaultTheme,
      home: LoginPage(),
    );
  }
}

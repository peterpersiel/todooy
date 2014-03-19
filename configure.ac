dnl Warning: This is an automatically generated file, do not edit!
dnl Process this file with autoconf to produce a configure script.
AC_PREREQ([2.54])
AC_INIT([Todooy], [0.1])
AM_INIT_AUTOMAKE([foreign])
AM_MAINTAINER_MODE

dnl pkg-config
AC_PATH_PROG(PKG_CONFIG, pkg-config, no)
if test "x$PKG_CONFIG" = "xno"; then
        AC_MSG_ERROR([You need to install pkg-config])
fi

SHAMROCK_EXPAND_LIBDIR
SHAMROCK_EXPAND_BINDIR
SHAMROCK_EXPAND_DATADIR

AC_PROG_INSTALL

AC_PATH_PROG(SMCS, smcs, no)
if test "x$SMCS" = "xno"; then
        AC_MSG_ERROR([smcs Not found])
fi


AC_ARG_ENABLE(debug_iphonesimulator,
	AC_HELP_STRING([--enable-debug_iphonesimulator],
		[Use 'DEBUG_IPHONESIMULATOR' Configuration [default=YES]]),
		enable_debug_iphonesimulator=yes, enable_debug_iphonesimulator=no)
AM_CONDITIONAL(ENABLE_DEBUG_IPHONESIMULATOR, test x$enable_debug_iphonesimulator = xyes)
if test "x$enable_debug_iphonesimulator" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
AC_ARG_ENABLE(release_iphonesimulator,
	AC_HELP_STRING([--enable-release_iphonesimulator],
		[Use 'RELEASE_IPHONESIMULATOR' Configuration [default=NO]]),
		enable_release_iphonesimulator=yes, enable_release_iphonesimulator=no)
AM_CONDITIONAL(ENABLE_RELEASE_IPHONESIMULATOR, test x$enable_release_iphonesimulator = xyes)
if test "x$enable_release_iphonesimulator" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
AC_ARG_ENABLE(debug_iphone,
	AC_HELP_STRING([--enable-debug_iphone],
		[Use 'DEBUG_IPHONE' Configuration [default=NO]]),
		enable_debug_iphone=yes, enable_debug_iphone=no)
AM_CONDITIONAL(ENABLE_DEBUG_IPHONE, test x$enable_debug_iphone = xyes)
if test "x$enable_debug_iphone" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
AC_ARG_ENABLE(release_iphone,
	AC_HELP_STRING([--enable-release_iphone],
		[Use 'RELEASE_IPHONE' Configuration [default=NO]]),
		enable_release_iphone=yes, enable_release_iphone=no)
AM_CONDITIONAL(ENABLE_RELEASE_IPHONE, test x$enable_release_iphone = xyes)
if test "x$enable_release_iphone" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
if test -z "$CONFIG_REQUESTED" ; then
	AM_CONDITIONAL(ENABLE_DEBUG_IPHONESIMULATOR, true)
	enable_debug_iphonesimulator=yes
fi


dnl package checks, common for all configs

dnl package checks, per config


AC_CONFIG_FILES([
todooy
Makefile

])

AC_OUTPUT

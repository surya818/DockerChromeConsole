#! /bin/bash
rm -f /tmp/.X*
ps -ef|grep chrome|awk '{ print $1 }'|xargs kill -9
ps -ef|grep Xvfb|awk '{ print $1 }'|xargs kill -9
ps -ef|grep chromium|awk '{ print $1 }'|xargs kill -9
Xvfb -ac :1 -screen 0 1280x1024x16 &
unset DISPLAY
export DISPLAY=:1

// @ts-nocheck
// Note: type annotations allow type checking and IDEs autocompletion

const lightCodeTheme = require('prism-react-renderer/themes/github');
const darkCodeTheme = require('prism-react-renderer/themes/dracula');
const simplePlantUML = require('@akebifiky/remark-simple-plantuml');

/** @type {import('@docusaurus/types').Config} */
const config = {
  title: 'Profio Application',
  tagline: '🚛 A Modern Logistics Management System ✈️',
  url: 'https://www.cjlogistics.com/en/main',
  baseUrl: '/',
  onBrokenLinks: 'throw',
  onBrokenMarkdownLinks: 'warn',
  favicon: 'img/favicon.ico',
  organizationName: 'HutechCJ', // Usually your GitHub org/user name.
  projectName: 'ProfioApp', // Usually your repo name.
  presets: [
    [
      'classic',
      /** @type {import('@docusaurus/preset-classic').Options} */
      ({
        docs: {
          sidebarPath: require.resolve('./sidebars.js'),
          editUrl: 'https://github.com/HutechCJ/ProfioApp',
          remarkPlugins: [simplePlantUML],
        },
        blog: {
          showReadingTime: true,
          editUrl: 'https://github.com/HutechCJ/ProfioApp',
        },
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
        },
      }),
    ],
  ],
  themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
    ({
      zoom: {
        selector: '.markdown :not(em) > img',
        config: {
          background: {
            light: 'rgb(255, 255, 255)',
            dark: 'rgb(50, 50, 50)',
          },
        },
      },
      announcementBar: {
        id: 'announcement-bar',
        content:
          '<a target="_blank" rel="nofollow noopener noreferrer" href="https://github.com/HutechCJ/ProfioApp">⭐ Star Application on GitHub</a>',
        isCloseable: true,
      },
      image: 'img/web-preview.jpg',
      navbar: {
        title: 'Profio Application',
        logo: {
          alt: 'CJ Logo',
          src: 'img/logo.png',
        },
        items: [
          {
            type: 'doc',
            docId: 'intro',
            position: 'left',
            label: 'Documentation',
          },
          {
            href: 'https://www.cjlogistics.com/en/main',
            label: 'Website',
            position: 'left',
            target: '_self',
          },
          {
            href: 'https://github.com/HutechCJ/ProfioApp',
            label: 'GitHub',
            position: 'right',
          },
        ],
      },
      footer: {
        style: 'dark',
        links: [
          {
            title: 'Company',
            items: [
              {
                label: 'About',
                href: 'https://www.cjlogistics.com/en/about/brand/message',
              },
              {
                label: 'Library',
                href: 'https://www.cjlogistics.com/en/about/brand/story',
              },
              {
                label: 'Sponsorship',
                href: 'https://www.cjlogistics.com/en/about/sponsorship/motorsports',
              },
              {
                label: 'Investor Relations',
                href: 'https://www.cjlogistics.com/en/page/investment/composition',
              },
              {
                label: 'Recruitment',
                href: 'https://www.cjlogistics.com/en/page/about/cj-way',
              },
            ],
          },
          {
            title: 'Legal',
            items: [
              {
                label: 'Privacy Policy',
                href: 'https://www.cjlogistics.com/en/agreement/privacy-policy',
              },
              {
                label: 'Operation Policy',
                href: 'https://www.cjlogistics.com/en/agreement/operation-management-policy',
              },
            ],
          },
          {
            title: 'Support',
            items: [
              {
                label: 'Business Inquiry',
                href: 'https://www.cjlogistics.com/en/support/contacts',
              },
              {
                label: 'FAQ',
                href: 'https://www.cjlogistics.com/en/support/faq',
              },
              {
                label: '1:1 Assistance',
                href: 'https://www.cjlogistics.com/en/support/inquiry/agreement',
              },
            ],
          },
          {
            title: 'More',
            items: [
              {
                label: 'CJ Vietnam',
                href: 'https://cjvietnam.vn/vi/about/overview',
              },
              {
                label: 'Hutech University',
                href: 'https://hutech.edu.vn/',
              },
              {
                label: 'Hutech CJ',
                href: 'https://github.com/HutechCJ',
              },
            ],
          },
        ],
        copyright: `Copyright © ${new Date().getFullYear()} HutechCJ. All rights reserved`,
        logo: {
          alt: 'CJ Logo',
          src: 'img/logo.png',
          href: 'https://github.com/HutechCJ',
          width: 100,
        },
      },
      prism: {
        theme: lightCodeTheme,
        darkTheme: darkCodeTheme,
      },
      colorMode: {
        defaultMode: 'light',
        disableSwitch: true,
        respectPrefersColorScheme: false,
      },
    }),
};

module.exports = config;

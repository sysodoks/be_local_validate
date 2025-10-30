using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace aspx_app  // ȷ�������ռ�����Ŀ����һ��
{
    public partial class Form1 : Form
    {
        // �ؼ�����
        private TextBox txtInput;
        private Button btnEncrypt;
        private TextBox txtCiphertext;
        private Button btnDecrypt;
        private TextBox txtPlaintext;
        private RichTextBox rtbEncryptFunc;
        private RichTextBox rtbDecryptFunc;

        public Form1()
        {
            // �ֶ���ʼ������������������ɵ�InitializeComponent��
            this.SuspendLayout();
            InitControls();  // �Զ���ؼ���ʼ��
            this.ResumeLayout(false);
        }

        // ��ʼ�����пؼ��������������ɵĴ��룩
        private void InitControls()
        {
            // ��������
            this.Text = "�ӽ���Э����֤����";
            this.Size = new System.Drawing.Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // ��߼��ܺ���չʾ��
            rtbEncryptFunc = new RichTextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(470, 200),
                ReadOnly = false,  // �����û��༭����Э��
                Font = new System.Drawing.Font("Consolas", 9F),
                Text = @"// ����Э�飨���޸ģ�
private byte[] Encrypt(byte[] data)
{
    string upload = ""-----------------------------7e6103b1815de Content-Disposition:form-data;name=\""uploadFile\"";filename=\""test.png\"" Content-Type:application/octet-stream  DaYer0 -----------------------------7e6103b1815de--"";
    string base64Data = Convert.ToBase64String(data).Replace(""+"", ""<"").Replace(""/"", ""> "");
    upload = upload.Replace(""DaYer0"", base64Data);
    return Encoding.UTF8.GetBytes(upload);
}"
            };
            this.Controls.Add(rtbEncryptFunc);

            // �ұ߽��ܺ���չʾ��
            rtbDecryptFunc = new RichTextBox
            {
                Location = new System.Drawing.Point(500, 10),
                Size = new System.Drawing.Size(470, 200),
                ReadOnly = false,  // �����û��༭����Э��
                Font = new System.Drawing.Font("Consolas", 9F),
                Text = @"// ����Э�飨���޸ģ�
private byte[] Decrypt(byte[] data)
{
    using (MemoryStream ms = new MemoryStream())
    {
        ms.Write(data, 150, data.Length - 195);
        byte[] validBytes = ms.ToArray();
        string base64Str = Encoding.UTF8.GetString(validBytes).Replace(""<"", ""+"").Replace(""> "",""/"");
        return Convert.FromBase64String(base64Str);
    }
}"
            };
            this.Controls.Add(rtbDecryptFunc);

            // �����
            txtInput = new TextBox
            {
                Location = new System.Drawing.Point(10, 240),
                Size = new System.Drawing.Size(250, 80),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                PlaceholderText = "������Ҫ���ܵ�����"
            };
            this.Controls.Add(txtInput);

            // ���ܰ�ť
            btnEncrypt = new Button
            {
                Location = new System.Drawing.Point(270, 240),
                Size = new System.Drawing.Size(80, 40),
                Text = "����"
            };
            btnEncrypt.Click += BtnEncrypt_Click;
            this.Controls.Add(btnEncrypt);

            // ���������
            txtCiphertext = new TextBox
            {
                Location = new System.Drawing.Point(360, 240),
                Size = new System.Drawing.Size(250, 80),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };
            this.Controls.Add(txtCiphertext);

            // ���ܰ�ť
            btnDecrypt = new Button
            {
                Location = new System.Drawing.Point(620, 240),
                Size = new System.Drawing.Size(80, 40),
                Text = "����"
            };
            btnDecrypt.Click += BtnDecrypt_Click;
            this.Controls.Add(btnDecrypt);

            // ���������
            txtPlaintext = new TextBox
            {
                Location = new System.Drawing.Point(710, 240),
                Size = new System.Drawing.Size(250, 80),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };
            this.Controls.Add(txtPlaintext);
        }

        // ���ܰ�ť����¼�
        private void BtnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                MessageBox.Show("������Ҫ���ܵ�����", "��ʾ");
                return;
            }
            try
            {
                byte[] inputData = Encoding.UTF8.GetBytes(txtInput.Text);
                byte[] cipherData = Encrypt(inputData);
                txtCiphertext.Text = Encoding.UTF8.GetString(cipherData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"����ʧ��: {ex.Message}", "����");
            }
        }

        // ���ܰ�ť����¼�
        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCiphertext.Text))
            {
                MessageBox.Show("����ִ�м��ܲ���", "��ʾ");
                return;
            }
            try
            {
                byte[] cipherData = Encoding.UTF8.GetBytes(txtCiphertext.Text);
                byte[] plainData = Decrypt(cipherData);
                txtPlaintext.Text = Encoding.UTF8.GetString(plainData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"����ʧ��: {ex.Message}", "����");
            }
        }

        // ����Э��ʵ�֣�JSPת���棩
        private byte[] Encrypt(byte[] data)
        {
            string upload = "-----------------------------7e6103b1815de Content-Disposition:form-data;name=\"uploadFile\";filename=\"test.png\" Content-Type:application/octet-stream  DaYer0 -----------------------------7e6103b1815de--";
            string base64Data = Convert.ToBase64String(data).Replace("+", "<").Replace("/", ">");
            upload = upload.Replace("DaYer0", base64Data);
            return Encoding.UTF8.GetBytes(upload);
        }

        // ����Э��ʵ�֣�JSPת���棩
        private byte[] Decrypt(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(data, 150, data.Length - 195);
                byte[] validBytes = ms.ToArray();
                string base64Str = Encoding.UTF8.GetString(validBytes).Replace("<", "+").Replace(">", "/");
                return Convert.FromBase64String(base64Str);
            }
        }
    }
}